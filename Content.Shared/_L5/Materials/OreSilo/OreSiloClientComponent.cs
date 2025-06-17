namespace Content.Shared.Materials.OreSilo;

public sealed partial class OreSiloClientComponent
{
    /// <summary>
    /// Whether this is a silo that only pushes materials (ore processor)
    /// </summary>
    [DataField]
    public bool Source;

    /// <summary>
    /// If this is a <see cref="Source"/>, this temporarily disables its
    /// material pushing.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool Enabled = true;
}
