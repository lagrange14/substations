using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._L5.Traits.HUD
{
    [RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
    public abstract partial class ToggleComponent : Component
    {
        [DataField]
        public abstract EntProtoId ToggleProto { get; set; }

        [DataField, AutoNetworkedField]
        public EntProtoId? ToggleAction;

        [DataField, AutoNetworkedField]
        public EntityUid? Action;

        [DataField]
        public virtual SoundSpecifier? ToggleOnSound { get; set; }

        [DataField]
        public virtual SoundSpecifier? ToggleOffSound { get; set; }

        [DataField, AutoNetworkedField]
        public bool Enabled { get; set; } = true;
    }
}
