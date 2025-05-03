using Content.Shared.Database;
using Content.Shared.Research.Components;

namespace Content.Server.Research.Systems;

public sealed partial class ResearchSystem
{
    private int _pointCost = 10_000;

    /// <summary>
    /// L5 - purchase a bluespace crystal using research points
    /// </summary>
    public void PurchaseCrystal(EntityUid client,
        EntityUid user,
        ResearchClientComponent? component = null,
        TechnologyDatabaseComponent? clientDatabase = null)
    {
        if (!Resolve(client, ref component, ref clientDatabase, false))
            return;

        if (!TryGetClientServer(client, out var serverEnt, out var server, component))
            return;

        if (server.Points < _pointCost)
            return;

        ModifyServerPoints(serverEnt.Value, -_pointCost); // TODO make CVar
        SpawnNextToOrDrop("MaterialBluespace1", client);

        _adminLog.Add(LogType.Action, LogImpact.Medium,
            $"{ToPrettyString(user):player} purchased a bluespace crystal using research points.");
    }
}
