using Content.Shared.Actions;
using Content.Shared.Contraband;
using Content.Shared.Overlays;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._L5.Traits.HUD
{
    [RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
    [Access(typeof(ToggleSecHudSystem))]
    public sealed partial class ToggleSecHudComponent : Component
    {
        [DataField]
        public EntProtoId ToggleProto = "ActionToggleSecHud";

        [DataField, AutoNetworkedField]
        public EntProtoId? ToggleSecHudAction;

        [DataField, AutoNetworkedField]
        public EntityUid? Action;

        [DataField, AutoNetworkedField]
        public bool Enabled { get; set; } = true;
    }

    // TODO : Behavior to actually, you know. call the below functions? Actions.
    // When this system is added to an entity, we need to add an associated action that toggles the components.

    public abstract class ToggleSystem : EntitySystem
    {
        // Move initialization to here, making it more generic and all that
        protected void TryToggleComp<T>(Entity<ToggleSecHudComponent> entity) where T : Component, new()
        {
            if (entity.Comp.Enabled && !HasComp<T>(entity))
                AddComp<T>(entity);
            else if (!entity.Comp.Enabled && HasComp<T>(entity))
                RemComp<T>(entity);
        }
    }

    public sealed class ToggleSecHudSystem() : ToggleSystem
    {
        [Dependency] private readonly SharedActionsSystem _actionsSystem = default!;
        public override void Initialize()
        {
            SubscribeLocalEvent<ToggleSecHudComponent, ComponentStartup>(OnComponentAdded);
            SubscribeLocalEvent<ToggleSecHudComponent, ComponentShutdown>(OnComponentRemoved);
            SubscribeLocalEvent<ToggleSecHudComponent, ToggleSecHudEvent>(OnSecHudEvent);
        }

        private void OnSecHudEvent(Entity<ToggleSecHudComponent> ent, ref ToggleSecHudEvent args)
        {
            if (args.Handled)
                return;

            ent.Comp.Enabled ^= true; // Flip the enabled bit
            TryToggle(ent);

            args.Handled = true;
        }

        private void OnComponentAdded(Entity<ToggleSecHudComponent> ent, ref ComponentStartup args)
        {
            ent.Comp.Enabled = true;
            TryToggle(ent);

            if (!string.IsNullOrWhiteSpace(ent.Comp.ToggleSecHudAction) && ent.Comp.Action == null)
                _actionsSystem.AddAction(ent, ref ent.Comp.Action, ent.Comp.ToggleSecHudAction);
        }

        private void OnComponentRemoved(Entity<ToggleSecHudComponent> ent, ref ComponentShutdown args)
        {
            ent.Comp.Enabled = false;
            TryToggle(ent);

            if (ent.Comp.Action != null)
                _actionsSystem.RemoveAction(ent.Comp.Action);
        }

        public void TryToggle(Entity<ToggleSecHudComponent> entity)
        {
            TryToggleComp<ShowJobIconsComponent>(entity);
            TryToggleComp<ShowMindShieldIconsComponent>(entity);
            TryToggleComp<ShowCriminalRecordIconsComponent>(entity);
            TryToggleComp<ShowContrabandDetailsComponent>(entity);
        }
    }

    public sealed partial class ToggleSecHudEvent : InstantActionEvent
    {

    }
}
