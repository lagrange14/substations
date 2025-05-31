using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Shared._L5.Traits.HUD
{
    [RegisterComponent]
    public sealed partial class ToggleMedHudComponent : ToggleComponent
    {
        [DataField]
        public override EntProtoId ToggleProto { get; set; } = "ActionToggleMedHud";

        [DataField]
        public override SoundSpecifier? ToggleOnSound { get; set; } = new SoundPathSpecifier("/Audio/_L5/Effects/HUD/MedHudEnable.ogg");
        [DataField]
        public override SoundSpecifier? ToggleOffSound { get; set; } = new SoundPathSpecifier("/Audio/_L5/Effects/HUD/MedHudDisable.ogg");
    }
}
