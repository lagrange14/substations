using Content.Server.Radio.Components;
using Content.Shared.Hands;
using Content.Shared.Verbs;

namespace Content.Server.Radio.EntitySystems;

public sealed partial class RadioDeviceSystem
{
    private void ToggleHandheldMic(Entity<RadioMicrophoneComponent, RadioSpeakerComponent> ent,
        EntityUid user)
    {
        var micComp = ent.Comp1;
        var speakerState = ent.Comp2.Enabled;
        var newMicState = !micComp.Enabled;

        if (!speakerState)
        {
            // The handheld is "off", so don't change anything and tell the user. This is probably unreachable.
            _popup.PopupEntity(Loc.GetString("handheld-radio-mute-button-useless"), ent, user);
            return;
        }

        micComp.EnabledAutomatically = false;
        SetMicrophoneEnabled(ent, user, newMicState, true, micComp);
        var message = Loc.GetString(newMicState ? "handheld-radio-mic-enable" : "handheld-radio-mic-disable");
        _popup.PopupEntity(message, ent, user);
    }

    private void ToggleHandheldSpeaker(Entity<RadioMicrophoneComponent, RadioSpeakerComponent> ent,
        EntityUid user)
    {
        var micComp = ent.Comp1;
        var speakerComp = ent.Comp2;
        var newSpeakerState = !speakerComp.Enabled;

        // Toggling the speaker (AKA its "power") always disables the mic.
        SetMicrophoneEnabled(ent, user, false, true, micComp);
        SetSpeakerEnabled(ent, user, newSpeakerState, true, speakerComp);

        var message = Loc.GetString(newSpeakerState ? "handheld-radio-enable" : "handheld-radio-disable");
        _popup.PopupEntity(message, ent, user);
    }

    private void OnGotMicEquipped(Entity<RadioMicrophoneComponent> ent, ref GotEquippedHandEvent args)
    {
        if (!ent.Comp.ToggleOnVerb || ent.Comp.Enabled)
            return;

        ent.Comp.EnabledAutomatically = true;
        SetMicrophoneEnabled(ent, args.User, true, true);
    }

    private void OnGotMicUnequipped(Entity<RadioMicrophoneComponent> ent, ref GotUnequippedHandEvent args)
    {
        if (!ent.Comp.ToggleOnVerb || !ent.Comp.EnabledAutomatically)
            return;

        SetMicrophoneEnabled(ent, args.User, false, true);
    }

    private void OnGetMicVerbs(Entity<RadioMicrophoneComponent> ent, ref GetVerbsEvent<ActivationVerb> args)
    {
        if (!args.CanAccess || !args.CanInteract || !ent.Comp.ToggleOnVerb)
            return;

        // For now only handling mic+radio, as this otherwise changes the interactions one would expect.
        if (!TryComp<RadioSpeakerComponent>(ent, out var speakerComp))
            return;

        // Verb is useless if speaker is off.
        if (!speakerComp.Enabled)
            return;

        var micComp = ent.Comp;
        var user = args.User;
        args.Verbs.Add(new ActivationVerb
        {
            Text = Loc.GetString($"handheld-radio-mic-verb-{(ent.Comp.Enabled ? "disable" : "enable")}"),
            Act = () => ToggleHandheldMic((ent, micComp, speakerComp), user),
            Priority = 1,
        });
    }

    private void OnGetSpeakerVerbs(Entity<RadioSpeakerComponent> ent, ref GetVerbsEvent<AlternativeVerb> args)
    {
        if (!args.CanAccess || !args.CanInteract || !ent.Comp.ToggleOnVerb)
            return;

        if (!TryComp<RadioMicrophoneComponent>(ent, out var micComp))
            return;

        var speakerComp = ent.Comp;
        var user = args.User;
        args.Verbs.Add(new AlternativeVerb
        {
            Text = Loc.GetString($"handheld-radio-verb-{(ent.Comp.Enabled ? "disable" : "enable")}"),
            Act = () => ToggleHandheldSpeaker((ent, micComp, speakerComp), user),
        });
    }
}
