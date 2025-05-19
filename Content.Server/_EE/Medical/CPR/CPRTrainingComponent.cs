using Content.Shared.Damage;
using Robust.Shared.Audio;
// ReSharper disable InconsistentNaming

namespace Content.Server._EE.Medical.CPR;

[RegisterComponent]
public sealed partial class CPRTrainingComponent : Component
{
    [DataField]
    public SoundSpecifier CPRSound = new SoundPathSpecifier("/Audio/Effects/CPR.ogg");

    [DataField]
    public TimeSpan DoAfterDuration = TimeSpan.FromSeconds(3);

    [DataField] public float CrackRibsModifier = 1f;

    public EntityUid? CPRPlayingStream;
}
