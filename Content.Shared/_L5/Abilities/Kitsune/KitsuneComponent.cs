using Content.Shared.Cloning;
using Robust.Shared.Prototypes;

namespace Content.Shared._DV.Abilities.Kitsune;

public sealed partial class KitsuneComponent
{
    /// <summary>
    /// Components to be copied over to the fox form
    /// </summary>
    [DataField]
    public ProtoId<CloningSettingsPrototype> ClonePrototype = "BaseClone";
}
