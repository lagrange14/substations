using Content.Server.Popups;
using Content.Shared._L5.CCVar;
using Content.Shared._L5.Traits.HardOfHearing;
using Content.Shared.Chat;
using Content.Shared.Database;
using Content.Shared.Hands.Components;
using Content.Shared.IdentityManagement;
using Content.Shared.Popups;
using Robust.Shared.Network;
using Robust.Shared.Utility;

namespace Content.Server.Chat.Systems;

public sealed partial class ChatSystem
{
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly SignLanguageSystem _signLanguage = default!;

    private void SendEntitySign(
        EntityUid source,
        string action,
        ChatTransmitRange range,
        string? nameOverride,
        bool hideLog = false,
        bool ignoreActionBlocker = false,
        NetUserId? author = null
    )
    {
        if (!_signLanguage.CanSign(source))
            return; // Begone

        if (!_actionBlocker.CanEmote(source) && !ignoreActionBlocker)
            return;

        // Check if at least one hand is free.
        if (!TryComp<HandsComponent>(source, out var hands))
            return;
        if (hands.CountFreeHands() == 0)
        {
            _popup.PopupEntity(Loc.GetString("chat-manager-entity-sign-no-free-hands"),
                source,
                source,
                PopupType.SmallCaution);
            return;
        }

        // get the entity's apparent name (if no override provided).
        var ent = Identity.Entity(source, EntityManager);
        string name = FormattedMessage.EscapeText(nameOverride ?? Name(ent));

        // Signing uses Identity.Name, since it doesn't actually involve your voice at all.
        var wrappedMessage = Loc.GetString("chat-manager-entity-sign-wrap-message",
            ("color", Color.LightSteelBlue.ToHex()),
            ("entityName", name),
            ("entity", ent),
            ("message", FormattedMessage.RemoveMarkupOrThrow(action)));

        // Those who don't understand sign language see only that something was signed.
        var wrappedObfuscatedMessage = Loc.GetString("chat-manager-entity-unknown-sign-wrap-message",
            ("color", Color.LightSteelBlue.ToHex()),
            ("entityName", name),
            ("entity", ent));


        SendInVoiceRange(
            ChatChannel.Sign,
            name,
            action,
            wrappedMessage,
            obfuscated: "",
            obfuscatedWrappedMessage: wrappedObfuscatedMessage,
            source,
            range,
            author,
            checkLOS: _configurationManager.GetCVar(L5CCVars.SignLanguageRespectsLOS)
        );
        if (!hideLog)
            if (name != Name(source))
                _adminLogger.Add(LogType.Chat, LogImpact.Low, $"Sign language statement from {ToPrettyString(source):user} as {name}: {action}");
            else
                _adminLogger.Add(LogType.Chat, LogImpact.Low, $"Sign language statement from {ToPrettyString(source):user}: {action}");
    }
}
