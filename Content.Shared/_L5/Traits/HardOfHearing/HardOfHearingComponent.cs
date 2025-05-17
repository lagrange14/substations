namespace Content.Shared._L5.Traits.HardOfHearing;

[RegisterComponent]
public sealed partial class HardOfHearingComponent : Component
{
    /// <summary>
    /// Determines whether the character is profoundly deaf, with the mechanical impact of not hearing any sounds, not
    /// just language being reduced to whisper range and obfuscated otherwise.
    /// </summary>
    [DataField]
    public bool ProfoundlyDeaf;
}
