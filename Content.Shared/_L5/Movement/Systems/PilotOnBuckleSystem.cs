using Content.Shared._L5.Movement.Components;
using Content.Shared.Buckle.Components;
using Content.Shared.Movement.Components;
using Content.Shared.Movement.Systems;
using Content.Shared.Physics;
using Robust.Shared.Physics.Components;
using Robust.Shared.Physics;
using Robust.Shared.Physics.Systems;
using System.Linq;

namespace Content.Shared._L5.Movement.Systems
{
    public sealed class PilotOnBuckleSystem() : EntitySystem
    {
        [Dependency] private readonly SharedMoverController _moverController = default!;
        [Dependency] private readonly SharedPhysicsSystem _physics = default!;
        public override void Initialize()
        {
            // When strapping into an object, update entity associations.
            SubscribeLocalEvent<PilotOnBuckleComponent, StrappedEvent>(OnStrap);
            SubscribeLocalEvent<PilotOnBuckleComponent, UnstrappedEvent>(OnUnstrap);
        }

        /// <summary>
        /// When strapping into an object with an attach on sit component, update entity associations.
        /// </summary>
        private void OnStrap(Entity<PilotOnBuckleComponent> ent, ref StrappedEvent args)
        {
            var moverEntity = args.Buckle.Owner;
            var movedEntity = args.Strap.Owner;

            // We need the moved entity to be able to be moved and have a move speed to use.
            EnsureComp<InputMoverComponent>(movedEntity);
            EnsureComp<MovementSpeedModifierComponent>(movedEntity);

            // Physics 'bullshittery' necessary for object to behave properly (modified from AnimateSpellSystem)
            // For collision layer, the Opaque CG is used rather MobLayer to ensure bullets don't collide with the wheelchair (otherwise this totally becomes a moving riot shield)
            // Admins be warned - applying this to players will change their physics and *probably* makes them immune to bullets
            if (TryComp<FixturesComponent>(movedEntity, out var fixtures) && TryComp<PhysicsComponent>(movedEntity, out var physics))
            {
                var xform = Transform(movedEntity);
                var fixture = fixtures.Fixtures.First();

                _physics.SetCanCollide(movedEntity, true, true, false, fixtures, physics);
                _physics.SetCollisionMask(movedEntity, fixture.Key, fixture.Value, (int)CollisionGroup.MobMask, fixtures, physics);
                _physics.SetCollisionLayer(movedEntity, fixture.Key, fixture.Value, (int)CollisionGroup.Opaque, fixtures, physics);
                _physics.SetBodyType(movedEntity, BodyType.KinematicController, fixtures, physics, xform);
                _physics.SetBodyStatus(movedEntity, physics, BodyStatus.OnGround, true);
                _physics.SetFixedRotation(movedEntity, false, true, fixtures, physics);
                _physics.SetHard(movedEntity, fixture.Value, true, fixtures);
            }

            _moverController.SetRelay(moverEntity, movedEntity);
        }

        /// <summary>
        /// When unstrapping from a moved entity, update entity associations.
        /// TODO: Not important for initial PR, but technically we'd want to do this on AttachOnSitComponent removal.
        /// </summary>
        private void OnUnstrap(Entity<PilotOnBuckleComponent> ent, ref UnstrappedEvent args)
        {
            var moverEntity = args.Buckle.Owner;
            var movedEntity = args.Strap.Owner;
            RemCompDeferred<RelayInputMoverComponent>(moverEntity);
            RemCompDeferred<MovementRelayTargetComponent>(movedEntity);
        }
    }
}
