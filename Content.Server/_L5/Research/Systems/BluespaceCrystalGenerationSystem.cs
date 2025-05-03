using Content.Server.Popups;
using Content.Server.Research.Components;
using Content.Server.Research.Systems;
using Content.Shared._L5.Research;
using Content.Shared.Access.Components;
using Content.Shared.Access.Systems;

namespace Content.Server._L5.Research.Systems;

public sealed class BluespaceCrystalGenerationSystem : EntitySystem
{
    [Dependency] private readonly AccessReaderSystem _accessReader = default!;
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly ResearchSystem _research = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ResearchConsoleComponent, GenerateCrystalMessage>(OnGenerateCrystal);
    }

    /// <summary>
    /// Try to purchase a bluespace crystal using points
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="component"></param>
    /// <param name="args"></param>
    private void OnGenerateCrystal(EntityUid uid,
        ResearchConsoleComponent component,
        GenerateCrystalMessage args)
    {
        var act = args.Actor;

        if (TryComp<AccessReaderComponent>(uid, out var access) && !_accessReader.IsAllowed(act, uid, access))
        {
            _popup.PopupEntity(Loc.GetString("research-console-no-access-popup"), act);
            return;
        }

        _research.GenerateCrystal(uid, args.Actor);
    }
}
