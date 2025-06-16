using Content.Shared.DoAfter;
using Content.Shared.Tools;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.SubFloor;

public sealed partial class SubFloorHideComponent
{
    /// <summary>
    /// Whether or not there should be a verb to allow disabling this component.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool Toggleable;

    [ViewVariables(VVAccess.ReadWrite), AutoNetworkedField]
    public bool Enabled = true;

    [DataField]
    public ProtoId<ToolQualityPrototype> QualityNeeded = "Welding";
}

[Serializable, NetSerializable]
public sealed partial class TryHideVentUnderSubfloorEvent : SimpleDoAfterEvent;
