using Robust.Shared.Utility;

namespace Content.Shared.Chat.TypingIndicator;

public sealed partial class TypingIndicatorPrototype
{
    /// <summary>
    /// Sprite path for synth variant of talk sprite.
    /// </summary>
    [DataField]
    public ResPath SynthSpritePath = new("/Textures/_L5/Effects/speech_synth.rsi");

    /// <summary>
    /// Whether there is a synth variant for this talk sprite. This includes if
    /// the "variant" is just a fallback synthy-looking sprite that's used instead.
    /// </summary>
    [DataField]
    public bool HasSynthVariant;

    /// <summary>
    /// Whether there is intentionally no synth variant for this talk sprite.
    /// This is for things like writing indicators where we just want to keep
    /// the default behavior.
    /// </summary>
    [DataField]
    public bool NoSynthVariant;

    /// <summary>
    /// A fallback state to use for new species.
    /// </summary>
    [DataField]
    public string SynthFallbackState = "default0";

    /// <summary>
    /// The idle state sprite for synths.
    /// </summary>
    [DataField]
    public string SynthIdleState = "thinking";
}
