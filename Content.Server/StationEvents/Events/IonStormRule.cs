using Content.Server.Popups;
using Content.Server.Silicons.Laws;
using Content.Server.StationEvents.Components;
using Content.Shared._L5.Traits.Synthetic;
using Content.Shared.GameTicking.Components;
using Content.Shared.Popups;
using Content.Shared.Silicons.Laws.Components;
using Content.Shared.Station.Components;
using Robust.Shared.Random;

namespace Content.Server.StationEvents.Events;

public sealed class IonStormRule : StationEventSystem<IonStormRuleComponent>
{
    [Dependency] private readonly IonStormSystem _ionStorm = default!;
    [Dependency] private readonly PopupSystem _popup = default!;

    protected override void Started(EntityUid uid, IonStormRuleComponent comp, GameRuleComponent gameRule, GameRuleStartedEvent args)
    {
        base.Started(uid, comp, gameRule, args);

        if (!TryGetRandomStation(out var chosenStation))
            return;

        var query = EntityQueryEnumerator<SiliconLawBoundComponent, TransformComponent, IonStormTargetComponent>();
        while (query.MoveNext(out var ent, out var lawBound, out var xform, out var target))
        {
            // only affect law holders on the station
            if (CompOrNull<StationMemberComponent>(xform.GridUid)?.Station != chosenStation)
                continue;

            _ionStorm.IonStormTarget((ent, lawBound, target));
        }

        // L5 - synths: All synths get a tingly feeling
        var synthQuery = EntityQueryEnumerator<SynthComponent, TransformComponent>();
        while (synthQuery.MoveNext(out var ent, out var synth, out var xform))
        {
            if (RobustRandom.Prob(synth.AlertChance))
                continue;

            if (CompOrNull<StationMemberComponent>(xform.GridUid)?.Station != chosenStation)
                continue;

            _popup.PopupEntity(Loc.GetString("station-event-ion-storm-synth"), ent, ent, PopupType.Medium);
        }
        // end L5 - synths
    }
}
