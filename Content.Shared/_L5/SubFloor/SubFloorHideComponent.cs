namespace Content.Shared.SubFloor;

public sealed partial class SubFloorHideComponent
{
    /// <summary>
    /// Whether or not there should be a verb to allow disabling this component.
    /// </summary>
    [DataField]
    public bool Toggleable;

    [ViewVariables(VVAccess.ReadWrite)]
    public bool Enabled = true;
}
