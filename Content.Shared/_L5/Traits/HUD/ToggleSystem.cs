using Content.Shared.Actions;
using Content.Shared.Contraband;
using Content.Shared.Overlays;
using Robust.Shared.Audio.Systems;

namespace Content.Shared._L5.Traits.HUD
{
    // TODO : Behavior to actually, you know. call the below functions? Actions.
    // When this system is added to an entity, we need to add an associated action that toggles the components.

    public abstract class ToggleSystem<TCOMP> : EntitySystem where TCOMP : ToggleComponent
    {
        [Dependency] private readonly SharedActionsSystem _actionsSystem = default!;
        [Dependency] private readonly SharedAudioSystem _audio = default!;

        public override void Initialize()
        {
            SubscribeLocalEvent<TCOMP, ComponentStartup>(OnComponentAdded);
            SubscribeLocalEvent<TCOMP, ComponentShutdown>(OnComponentRemoved);
            SubscribeLocalEvent<TCOMP, ToggleEvent>(OnSecHudEvent);
        }

        private void OnSecHudEvent(Entity<TCOMP> ent, ref ToggleEvent args)
        {
            if (args.Handled) return;
            args.Handled = true;

            ent.Comp.Enabled ^= true; // Flip the enabled bit

            var sound = ent.Comp.Enabled ? ent.Comp.ToggleOnSound : ent.Comp.ToggleOffSound;
            if (sound != null)
                _audio.PlayPvs(sound, ent);

            TryUpdate(ent);
        }

        private void OnComponentAdded(Entity<TCOMP> ent, ref ComponentStartup args)
        {
            ent.Comp.Enabled = true;
            TryUpdate(ent);

            // If we somehow weren't able to load the action, try again using the cached ID
            if (string.IsNullOrWhiteSpace(ent.Comp.ToggleSecHudAction))
                ent.Comp.ToggleSecHudAction = ent.Comp.ToggleProto;

            // Load the action if possible
            if (!string.IsNullOrWhiteSpace(ent.Comp.ToggleSecHudAction) && ent.Comp.Action == null)
                _actionsSystem.AddAction(ent, ref ent.Comp.Action, ent.Comp.ToggleSecHudAction);
        }

        private void OnComponentRemoved(Entity<TCOMP> ent, ref ComponentShutdown args)
        {
            ent.Comp.Enabled = false;
            TryUpdate(ent);

            if (ent.Comp.Action != null)
                _actionsSystem.RemoveAction(ent.Comp.Action);
        }

        private void TryUpdate(Entity<TCOMP> entity)
        {
            TryUpdateComp<ShowJobIconsComponent>(entity);
            TryUpdateComp<ShowMindShieldIconsComponent>(entity);
            TryUpdateComp<ShowCriminalRecordIconsComponent>(entity);
            TryUpdateComp<ShowContrabandDetailsComponent>(entity);
        }

        private void TryUpdateComp<T>(Entity<TCOMP> entity) where T : Component, new()
        {
            if (entity.Comp.Enabled && !HasComp<T>(entity))
                AddComp<T>(entity);
            else if (!entity.Comp.Enabled && HasComp<T>(entity))
                RemComp<T>(entity);
        }
    }
}
