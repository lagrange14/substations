using Content.Server.Body.Systems;
using Content.Server.Medical;
using Content.Server.Popups;
using Content.Shared._L5.Traits.DietaryRestriction;
using Content.Shared.Chemistry.Components;
using Content.Shared.Popups;
using Content.Shared.Whitelist;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Server._L5.Traits.DietaryRestriction;

public sealed class DietaryRestrictionSystem : EntitySystem
{
    [Dependency] private readonly BloodstreamSystem _bloodstream = default!;
    [Dependency] private readonly PopupSystem _popup = default!;
    [Dependency] private readonly IPrototypeManager _prototype = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly VomitSystem _vomit = default!;
    [Dependency] private readonly EntityWhitelistSystem _whitelist = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PescetarianRestrictionComponent, MapInitEvent>(InitPescetarianRestriction);
        SubscribeLocalEvent<VegetarianRestrictionComponent, MapInitEvent>(InitVegetarianRestriction);
        SubscribeLocalEvent<VeganRestrictionComponent, MapInitEvent>(InitVeganRestriction);

        SubscribeLocalEvent<KosherRestrictionComponent, MapInitEvent>(InitKosherRestriction);
        SubscribeLocalEvent<HalalRestrictionComponent, MapInitEvent>(InitHalalRestriction);

        SubscribeLocalEvent<DairyAllergyComponent, MapInitEvent>(InitDairyAllergy);
        SubscribeLocalEvent<GlutenAllergyComponent, MapInitEvent>(InitGlutenAllergy);
        SubscribeLocalEvent<LatexAllergyComponent, MapInitEvent>(InitLatexAllergy);
        SubscribeLocalEvent<NightshadeAllergyComponent, MapInitEvent>(InitNightshadeAllergy);
    }

    private void InitPescetarianRestriction(Entity<PescetarianRestrictionComponent> entity, ref MapInitEvent args)
    {
        var restriction = EnsureComp<DietaryRestrictionComponent>(entity);
        restriction.Restrictions.Add("PescetarianRestriction");
    }

    private void InitVegetarianRestriction(Entity<VegetarianRestrictionComponent> entity, ref MapInitEvent args)
    {
        var restriction = EnsureComp<DietaryRestrictionComponent>(entity);
        restriction.Restrictions.Add("VegetarianRestriction");
    }

    private void InitVeganRestriction(Entity<VeganRestrictionComponent> entity, ref MapInitEvent args)
    {
        var restriction = EnsureComp<DietaryRestrictionComponent>(entity);
        restriction.Restrictions.Add("VeganRestriction");
    }

    private void InitKosherRestriction(Entity<KosherRestrictionComponent> entity, ref MapInitEvent args)
    {
        var restriction = EnsureComp<DietaryRestrictionComponent>(entity);
        restriction.Restrictions.Add("KosherRestriction");
    }

    private void InitHalalRestriction(Entity<HalalRestrictionComponent> entity, ref MapInitEvent args)
    {
        var restriction = EnsureComp<DietaryRestrictionComponent>(entity);
        restriction.Restrictions.Add("HalalRestriction");
    }

    private void InitDairyAllergy(Entity<DairyAllergyComponent> entity, ref MapInitEvent args)
    {
        var restriction = EnsureComp<DietaryRestrictionComponent>(entity);
        restriction.Restrictions.Add("DairyAllergy");
    }

    private void InitGlutenAllergy(Entity<GlutenAllergyComponent> entity, ref MapInitEvent args)
    {
        var restriction = EnsureComp<DietaryRestrictionComponent>(entity);
        restriction.Restrictions.Add("GlutenAllergy");
    }

    private void InitLatexAllergy(Entity<LatexAllergyComponent> entity, ref MapInitEvent args)
    {
        var restriction = EnsureComp<DietaryRestrictionComponent>(entity);
        restriction.Restrictions.Add("LatexAllergy");
    }

    private void InitNightshadeAllergy(Entity<NightshadeAllergyComponent> entity, ref MapInitEvent args)
    {
        var restriction = EnsureComp<DietaryRestrictionComponent>(entity);
        restriction.Restrictions.Add("NightshadeAllergy");
    }

    public bool TryFood(EntityUid user, EntityUid food)
    {
        if (!TryComp<DietaryRestrictionComponent>(user, out var restrictions))
        {
            return true;
        }

        var hasVomited = false;
        foreach (var proto in restrictions.Restrictions)
        {
            if (!_prototype.TryIndex<DietaryRestrictionPrototype>(proto, out var restriction))
                continue;

            // If they CAN'T eat it, bail early.
            if (_whitelist.IsWhitelistPass(restriction.MustNotHave, food)
                || _whitelist.IsWhitelistFail(restriction.MustHave, food))
            {
                return false;
            }

            // If they're allergic, they can eat it, but it makes them sick.
            if (_whitelist.IsWhitelistPass(restriction.AllergicTo, food))
            {
                // Add histamine to the chemstream and potentially vomit.
                _bloodstream.TryAddToChemicals(user, new Solution("Histamine", restriction.HistamineAmount));
                if (_random.Prob(restriction.ChanceOfVomiting) && !hasVomited)
                {
                    _vomit.Vomit(user);
                    hasVomited = true;
                }
            }

            // If they don't want it, let them know.
            if (_whitelist.IsWhitelistFail(restriction.Exceptions, food)
                || _whitelist.IsWhitelistPass(restriction.DoesNotWant, food)
                || _whitelist.IsWhitelistFail(restriction.Wants, food))
            {
                // notify the user, give a chance of them vomiting.
                _popup.PopupEntity(Loc.GetString($"trait-dietary-restriction-{restriction.ID}-fail"), user, user, PopupType.MediumCaution);
                if (_random.Prob(restriction.ChanceOfVomiting) && !hasVomited)
                {
                    _vomit.Vomit(user);
                    hasVomited = true;
                }
            }
        }
        return true;
    }
}
