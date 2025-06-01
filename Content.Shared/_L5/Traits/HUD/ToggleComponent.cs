using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._L5.Traits.HUD
{
    [RegisterComponent, Virtual, NetworkedComponent, AutoGenerateComponentState]
    public partial class ToggleComponent : Component
    {
        [DataField("toggleAction", required: true)]
        public EntProtoId ToggleAction { get; set; }

        [DataField, AutoNetworkedField]
        public EntityUid? Action;

        [DataField]
        public SoundSpecifier? ToggleOnSound { get; set; }

        [DataField]
        public SoundSpecifier? ToggleOffSound { get; set; }

        [DataField, AutoNetworkedField]
        public bool Enabled { get; set; } = true;
    }
}
