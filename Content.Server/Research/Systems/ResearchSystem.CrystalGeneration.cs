using Content.Shared._L5.CCVar;
using Content.Shared.Database;
using Content.Shared.Research.Components;
using Robust.Shared.Configuration;

namespace Content.Server.Research.Systems;

public sealed partial class ResearchSystem
{
    [Dependency] private readonly IConfigurationManager _configurationManager = default!;

    /// <summary>
    /// L5 - generate a bluespace crystal using research points
    /// </summary>
    public void GenerateCrystal(EntityUid client,
        EntityUid user,
        ResearchClientComponent? component = null,
        TechnologyDatabaseComponent? clientDatabase = null)
    {
        if (!Resolve(client, ref component, ref clientDatabase, false))
            return;

        if (!TryGetClientServer(client, out var serverEnt, out var server, component))
            return;

        var _pointCost = _configurationManager.GetCVar(L5CCVars.BluespaceCrystalPointCost);
        if (server.Points < _pointCost)
            return;

        ModifyServerPoints(serverEnt.Value, -_pointCost); // TODO make CVar
        SpawnNextToOrDrop("MaterialBluespace1", client);

        _adminLog.Add(LogType.Action, LogImpact.Medium,
            $"{ToPrettyString(user):player} generated a bluespace crystal using research points.");
    }
}
