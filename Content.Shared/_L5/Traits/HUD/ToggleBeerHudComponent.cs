using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Shared._L5.Traits.HUD
{
    [RegisterComponent]
    public sealed partial class ToggleBeerHudComponent : ToggleComponent
    {
        [DataField]
        public override EntProtoId? ToggleProto { get; set; } = "ActionToggleBeerHud";

        [DataField]
        public override SoundSpecifier? ToggleOnSound { get; set; } = new SoundPathSpecifier("/Audio/_L5/Effects/HUD/BeerHudEnable.ogg");

        [DataField]
        public override SoundSpecifier? ToggleOffSound { get; set; } = new SoundPathSpecifier("/Audio/_L5/Effects/HUD/BeerHudDisable.ogg");
    }
}
