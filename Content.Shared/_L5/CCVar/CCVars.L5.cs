using Robust.Shared.Configuration;

namespace Content.Shared._L5.CCVar;

[CVarDefs]
public sealed partial class L5CCVars
{
    /// <summary>
    /// The cost in research points for generating a bluespace crystal
    /// </summary>
    public static readonly CVarDef<int> BluespaceCrystalPointCost =
        CVarDef.Create("l5.research.bluespace_crystal_cost", 20_000, CVar.REPLICATED);

    /// <summary>
    /// How far speech travels in space — defaults to approximately touch
    /// </summary>
    public static readonly CVarDef<float> InSpaceRange =
        CVarDef.Create("chat.in_space_range", 0.3f, CVar.ARCHIVE, "How far voice travels in space.");

    /// <summary>
    /// The lowest pressure at which sound can be heard. 10kPa was picked on a whim as the point at which um probably not.
    /// </summary>
    public static readonly CVarDef<float> MinSoundTransmitPressure =
        CVarDef.Create("chat.min_sound_transmit_presure",
            10f,
            CVar.ARCHIVE,
            "The minimum pressure for sound to transmit");

    /// <summary>
    /// Whether speech travels through solid walls
    /// </summary>
    public static readonly CVarDef<bool> SpeechRespectsLOS =
        CVarDef.Create("chat.speech_respsects_los",
            false,
            CVar.ARCHIVE,
            "Whether speech travels through walls or is obfuscated.");

    /// <summary>
    /// Whether emotes go through walls
    /// </summary>
    public static readonly CVarDef<bool> EmoteRespectsLOS =
        CVarDef.Create("chat.emote_respects_los",
            true,
            CVar.ARCHIVE,
            "Whether emotes travel through walls or are silenced.");

    /// <summary>
    /// Whether subtle OOC goes through walls
    /// </summary>
    public static readonly CVarDef<bool> SubtleOOCRespectsLOS =
        CVarDef.Create("chat.subtleooc_respects_los",
            true,
            CVar.ARCHIVE,
            "Whether subtle OOC travels through walls or is silenced.");

    /// <summary>
    /// Whether LOOC goes through walls
    /// </summary>
    public static readonly CVarDef<bool> LOOCRespectsLOS =
        CVarDef.Create("chat.looc_respects_los",
            false,
            CVar.ARCHIVE,
            "Whether LOOC travels through walls or is silenced.");
}
