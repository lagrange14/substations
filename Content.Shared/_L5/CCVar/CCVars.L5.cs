using Robust.Shared.Configuration;

namespace Content.Shared._L5.CCVar;

[CVarDefs]
public sealed partial class L5CCVars
{
    /// <summary>
    /// The cost in research points for generating a bluespace crystal
    /// </summary>
    public static readonly CVarDef<int> BluespaceCrystalPointCost =
        CVarDef.Create("l5.research.bluespace_crystal_cost", 20_000, CVar.REPLICATED);
}
