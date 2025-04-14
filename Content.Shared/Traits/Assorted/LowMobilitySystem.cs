using Content.Shared.Movement.Systems;

namespace Content.Shared.Traits.Assorted
{
    /// <summary>
    /// Applies low mobility to entities with a LowMobilityComponent.
    /// </summary>
    public sealed class LowMobilitySystem : EntitySystem
    {
        [Dependency] private readonly MovementSpeedModifierSystem _movementSpeedModifierSystem = default!;

        /// <summary>
        /// Initializes any necessary functionalities and registers for necessary events.
        /// Note that the base class automatically unsubscribes from events on shutdown.
        /// </summary>
        public override void Initialize()
        {
            // We need to adjust the base move speed when the low mobility component is added or removed
            SubscribeLocalEvent<LowMobilityComponent, ComponentStartup>(OnStartup);
            SubscribeLocalEvent<LowMobilityComponent, ComponentShutdown>(OnShutdown);
            SubscribeLocalEvent<LowMobilityComponent, RefreshMovementSpeedModifiersEvent>(OnRefreshMovementSpeedModifiers);
        }

        #region Event Wrappers
        private void OnStartup(EntityUid uid, LowMobilityComponent comp, ComponentStartup args) => RefreshMovementSpeed(uid, comp);
        private void OnShutdown(EntityUid uid, LowMobilityComponent comp, ComponentShutdown args) => RefreshMovementSpeed(uid, comp, true);
        private void OnRefreshMovementSpeedModifiers(EntityUid uid, LowMobilityComponent component, RefreshMovementSpeedModifiersEvent args) => RefreshMovementSpeedModifiers(component, args);
        #endregion

        #region Private Update Functions
        /// <summary>
        /// Indicates that movement speed should be refreshed for this entity.
        /// </summary>
        /// <param name="uid">ID of the entity to refresh the movement speed of.</param>
        /// <param name="comp">Low mobility component associated with entity.</param>
        /// <param name="isShutdown">Whether the component is shutting down. If so, ignore the component's multipliers in the final event call.</param>
        private void RefreshMovementSpeed(EntityUid uid, LowMobilityComponent comp, bool isShutdown = false)
        {
            // If we're shutting down, we need to ignore the component's modifiers (achieved by setting them to 100%).
            if (isShutdown)
                (comp.WalkSpeedMultiplier, comp.SprintSpeedMultiplier) = (1.0f, 1.0f);

            _movementSpeedModifierSystem.RefreshMovementSpeedModifiers(uid);
        }

        /// <summary>
        /// Refreshes the movement speed associated with an entity.
        /// </summary>
        /// <param name="uid">ID of the entity to refresh the movement speed of.</param>
        /// <param name="component">Low mobility component associated with entity.</param>
        /// <param name="args">Arguments needed for speed modification.</param>
        private static void RefreshMovementSpeedModifiers(LowMobilityComponent component, RefreshMovementSpeedModifiersEvent args)
        {
            args.ModifySpeed(component.WalkSpeedMultiplier, component.SprintSpeedMultiplier);
        }
        #endregion
    }
}
