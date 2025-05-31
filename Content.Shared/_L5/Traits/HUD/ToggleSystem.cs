using Content.Shared.Actions;
using Robust.Shared.Audio.Systems;

namespace Content.Shared._L5.Traits.HUD
{
    public abstract class ToggleSystem<TComp> : EntitySystem where TComp : ToggleComponent
    {
        [Dependency] private readonly SharedActionsSystem _actionsSystem = default!;
        [Dependency] private readonly SharedAudioSystem _audio = default!;

        public override void Initialize()
        {
            SubscribeLocalEvent<TComp, ComponentStartup>(OnComponentAdded);
            SubscribeLocalEvent<TComp, ComponentShutdown>(OnComponentRemoved);
            SubscribeLocalEvent<TComp, ToggleEvent>(OnToggleEvent);
        }

        private void OnToggleEvent(Entity<TComp> ent, ref ToggleEvent args)
        {
            if (args.Action != ent.Comp.Action || args.Handled) return;
            args.Handled = true;

            ent.Comp.Enabled ^= true; // Flip the enabled bit

            var sound = ent.Comp.Enabled ? ent.Comp.ToggleOnSound : ent.Comp.ToggleOffSound;
            if (sound != null)
                _audio.PlayPvs(sound, ent);

            TryUpdate(ent);
        }

        private void OnComponentAdded(Entity<TComp> ent, ref ComponentStartup args)
        {
            TryUpdate(ent);

            // Load the action if possible
            if (string.IsNullOrWhiteSpace(ent.Comp.ToggleAction)) return;
            _actionsSystem.AddAction(ent, ref ent.Comp.Action, ent.Comp.ToggleAction);
        }

        private void OnComponentRemoved(Entity<TComp> ent, ref ComponentShutdown args)
        {
            ent.Comp.Enabled = false;
            TryUpdate(ent);
            _actionsSystem.RemoveAction(ent.Comp.Action);
        }

        protected abstract void TryUpdate(Entity<TComp> entity);

        protected void TryUpdateComp<T>(Entity<TComp> entity) where T : Component, new()
        {
            if (entity.Comp.Enabled)
                EnsureComp<T>(entity);
            else
                RemComp<T>(entity);
        }
    }
}
