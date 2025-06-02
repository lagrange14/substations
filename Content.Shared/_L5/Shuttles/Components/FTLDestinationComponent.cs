using Content.Shared.Whitelist;

namespace Content.Shared.Shuttles.Components;

public sealed partial class FTLDestinationComponent
{
    /// <summary>
    /// A whitelist meant to only be used with docking shuttles.
    /// </summary>
    [ViewVariables, DataField, AutoNetworkedField]
    public EntityWhitelist? DockingShuttleWhitelist;
}
