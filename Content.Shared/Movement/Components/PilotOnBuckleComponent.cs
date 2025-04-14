using Content.Server.Movement.Systems;
using Robust.Shared.GameStates;

namespace Content.Shared.Movement.Components;

/// <summary>
/// Raises the engine movement inputs for a particular entity onto the designated entity
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState(true)]
[Access(typeof(PilotOnBuckleSystem))]
public sealed partial class PilotOnBuckleComponent : Component
{
    [ViewVariables, AutoNetworkedField]
    public EntityUid RelayEntity;
}
