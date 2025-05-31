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
}
