using Content.Shared.Tools.Systems;
using Content.Shared.Verbs;

namespace Content.Shared.SubFloor;

public abstract partial class SharedSubFloorHideSystem
{
    [Dependency] private readonly SharedToolSystem _tool = default!;

    private void OnGetVerbs(Entity<SubFloorHideComponent> ent, ref GetVerbsEvent<Verb> args)
    {
        // Doesn't really make sense to show this verb if it's not underneath something
        if (!ent.Comp.Toggleable || !ent.Comp.IsUnderCover || !args.Using.HasValue)
            return;

        var user = args.User;
        var tool = args.Using.Value;

        if (!_tool.HasQuality(tool, ent.Comp.QualityNeeded))
            return;

        args.Verbs.Add(new Verb
        {
            Priority = 0,
            Text = Loc.GetString("subfloor-disguise-" + (ent.Comp.Enabled ? "hide" : "reveal")),
            DoContactInteraction = true, // Det's gonna get you, ya vent hider
            Act = () => _tool.UseTool(tool,
                user,
                ent,
                10f,
                [ent.Comp.QualityNeeded],
                new TryHideVentUnderSubfloorEvent(),
                10f),
        });
    }

    private void OnHideVentUnderSubfloor(Entity<SubFloorHideComponent> ent, ref TryHideVentUnderSubfloorEvent args)
    {
        if (args.Cancelled)
            return;

        ent.Comp.Enabled = !ent.Comp.Enabled;
        Dirty(ent);
        if (TryComp<AppearanceComponent>(ent, out var appearance))
            Appearance.QueueUpdate(ent, appearance);
    }
}
