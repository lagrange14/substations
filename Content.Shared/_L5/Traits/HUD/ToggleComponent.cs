using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._L5.Traits.HUD
{
    [RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
    public partial class ToggleComponent : Component
    {
        [DataField]
        public EntProtoId ToggleProto = "ActionToggleSecHud";

        [DataField, AutoNetworkedField]
        public EntProtoId? ToggleSecHudAction;

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
