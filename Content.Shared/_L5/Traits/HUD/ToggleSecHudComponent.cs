using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Shared._L5.Traits.HUD
{
    [RegisterComponent, AutoGenerateComponentState]
    public sealed partial class ToggleSecHudComponent : ToggleComponent
    {
        [DataField]
        public override EntProtoId? ToggleProto { get; set; } = "ActionToggleSecHud";

        [DataField, AutoNetworkedField]
        public EntProtoId ToggleAction { get; set; }

        [DataField]
        public override SoundSpecifier? ToggleOnSound { get; set; } = new SoundPathSpecifier("/Audio/_L5/Effects/HUD/SecHudEnable.ogg");

        [DataField]
        public override SoundSpecifier? ToggleOffSound { get; set; } = new SoundPathSpecifier("/Audio/_L5/Effects/HUD/SecHudDisable.ogg");
    }
}
