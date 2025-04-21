using Robust.Shared.Prototypes;

namespace Content.Shared._L5.Traits.DietaryRestriction;

[RegisterComponent]
public sealed partial class DietaryRestrictionComponent : Component
{
    /// <summary>
    /// A list of restrictions that the character has on their diet.
    /// </summary>
    [DataField]
    public List<ProtoId<DietaryRestrictionPrototype>> Restrictions = new();
}

[RegisterComponent]
public sealed partial class PescetarianRestrictionComponent : Component
{
}

[RegisterComponent]
public sealed partial class VegetarianRestrictionComponent : Component
{
}

[RegisterComponent]
public sealed partial class VeganRestrictionComponent : Component
{
}

[RegisterComponent]
public sealed partial class KosherRestrictionComponent : Component
{
}

[RegisterComponent]
public sealed partial class HalalRestrictionComponent : Component
{
}

[RegisterComponent]
public sealed partial class DairyAllergyComponent : Component
{
}

[RegisterComponent]
public sealed partial class GlutenAllergyComponent : Component
{
}

[RegisterComponent]
public sealed partial class LatexAllergyComponent : Component
{
}

[RegisterComponent]
public sealed partial class NightshadeAllergyComponent : Component
{
}
