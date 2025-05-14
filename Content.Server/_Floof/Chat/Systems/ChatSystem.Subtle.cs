using Content.Shared.Chat;
using Content.Shared.Database;
using Content.Shared.IdentityManagement;
using Robust.Shared.Network;
using Robust.Shared.Utility;

namespace Content.Server.Chat.Systems;

public sealed partial class ChatSystem
{
    private void SendEntitySubtle(
        EntityUid source,
        string action,
        ChatTransmitRange range,
        string? nameOverride,
        bool hideLog = false,
        bool ignoreActionBlocker = false,
        NetUserId? author = null
    )
    {
        if (!_actionBlocker.CanEmote(source) && !ignoreActionBlocker)
            return;

        // get the entity's apparent name (if no override provided).
        var ent = Identity.Entity(source, EntityManager);
        string name = FormattedMessage.EscapeText(nameOverride ?? Name(ent));

        // Emotes use Identity.Name, since it doesn't actually involve your voice at all.
        var wrappedMessage = Loc.GetString("chat-manager-entity-subtle-wrap-message",
            ("entityName", name),
            ("entity", ent),
            ("message", FormattedMessage.RemoveMarkup(action)));

        foreach (var (session, data) in GetRecipients(source, WhisperClearRange))
        {
            if (session.AttachedEntity is not { Valid: true } listener)
                continue;

            if (MessageRangeCheck(session, data, range) == MessageRangeCheckResult.Disallowed)
                continue;

            _chatManager.ChatMessageToOne(ChatChannel.Emotes, action, wrappedMessage, source, false, session.Channel);
        }

        if (!hideLog)
            if (name != Name(source))
                _adminLogger.Add(LogType.Chat, LogImpact.Low, $"Subtle from {ToPrettyString(source):user} as {name}: {action}");
            else
                _adminLogger.Add(LogType.Chat, LogImpact.Low, $"Subtle from {ToPrettyString(source):user}: {action}");
    }


}
