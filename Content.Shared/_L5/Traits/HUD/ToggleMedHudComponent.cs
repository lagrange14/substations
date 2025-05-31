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
        public override SoundSpecifier? ToggleOnSound { get; set; } = new SoundPathSpecifier("/Audio/Weapons/Guns/Empty/empty.ogg");
        [DataField]
        public override SoundSpecifier? ToggleOffSound { get; set; } = new SoundPathSpecifier("/Audio/Weapons/Guns/Empty/empty.ogg");
    }
}
