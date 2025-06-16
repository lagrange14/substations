using Content.Shared.Verbs;

namespace Content.Shared.SubFloor;

public abstract partial class SharedSubFloorHideSystem
{
    private void OnGetVerbs(Entity<SubFloorHideComponent> ent, ref GetVerbsEvent<Verb> args)
    {
        // Doesn't really make sense to show this verb if it's not underneath something
        if (!ent.Comp.Toggleable || !ent.Comp.IsUnderCover)
            return;

        args.Verbs.Add(new Verb
        {
            Priority = 0,
            Text = Loc.GetString("subfloor-disguise-" + (ent.Comp.Enabled ? "hide" : "reveal")),
            DoContactInteraction = true, // Det's gonna get you, ya vent hider
            Act = () =>
            {
                ent.Comp.Enabled = !ent.Comp.Enabled;
                UpdateFloorCover(ent, ent.Comp);
            },
        });
    }
}
