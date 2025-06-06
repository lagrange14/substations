using Robust.Shared.Audio;

namespace Content.Server.Radio.Components;

public sealed partial class RadioMicrophoneComponent
{
    [DataField, ViewVariables]
    public bool ToggleOnVerb = false;

    /// <summary>
    /// When paired with a speaker, we want to model this mic like it's a PTT
    /// button. When held in hand, we override this, while preserving the original
    /// mute/unmute status as long as the user doesn't change it themselves.
    /// </summary>
    public bool EnabledAutomatically = false;

    /// <summary>
    ///  Microphone on sound, when used with ToggleOnVerb
    /// </summary>
    [DataField]
    public SoundSpecifier SoundActivate =
        new SoundPathSpecifier("/Audio/_L5/Items/radio_on.ogg", AudioParams.Default.WithVolume(-4));

    /// <summary>
    ///  Microphone off sound, when used with ToggleOnVerb
    /// </summary>
    [DataField]
    public SoundSpecifier SoundDeactivate =
        new SoundPathSpecifier("/Audio/_L5/Items/radio_off.ogg", AudioParams.Default.WithVolume(-4));
}
