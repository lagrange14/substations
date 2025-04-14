using Robust.Shared.GameStates;

namespace Content.Shared.Traits.Assorted;

/// <summary>
/// Determines the move speed that should be given to entities with low mobility.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState, Access(typeof(LowMobilitySystem))]
public sealed partial class LowMobilityComponent : Component
{
    /// <summary>
    /// Determines what the low mobility walk speed should be.
    /// </summary>
    [AutoNetworkedField, DataField("WalkSpeedMultiplier"), ViewVariables(VVAccess.ReadWrite)]
    public float WalkSpeedMultiplier = 0.2f;

    /// <summary>
    /// Determines what the low mobility base sprint speed should be.
    /// </summary>
    [AutoNetworkedField, DataField("SprintSpeedMultiplier"), ViewVariables(VVAccess.ReadWrite)]
    public float SprintSpeedMultiplier = 0.2f;
}
