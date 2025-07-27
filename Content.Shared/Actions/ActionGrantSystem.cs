using  Content.Shared.Inventory;

namespace Content.Shared.Actions;

/// <summary>
/// <see cref="ActionGrantComponent"/>
/// </summary>
public sealed partial class ActionGrantSystem : EntitySystem // L5 - made partial
{
    [Dependency] private readonly SharedActionsSystem _actions = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<ActionGrantComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<ActionGrantComponent, ComponentShutdown>(OnShutdown);
        SubscribeLocalEvent<ItemActionGrantComponent, GetItemActionsEvent>(OnItemGet);

        InitializeL5(); // L5 - duh
    }

    private void OnItemGet(Entity<ItemActionGrantComponent> ent, ref GetItemActionsEvent args)
    {

        if (!TryComp(ent.Owner, out ActionGrantComponent? grant))
            return;

        if (ent.Comp.ActiveIfWorn && (args.SlotFlags == null || args.SlotFlags == SlotFlags.POCKET))
            return;

        foreach (var action in grant.ActionEntities)
        {
            args.AddAction(action);
        }
    }

    // L5 - made generic
    private void OnMapInit<T>(Entity<T> ent, ref MapInitEvent args) where T : ActionGrantComponent
    {
        foreach (var action in ent.Comp.Actions)
        {
            EntityUid? actionEnt = null;
            _actions.AddAction(ent.Owner, ref actionEnt, action);

            if (actionEnt != null)
                ent.Comp.ActionEntities.Add(actionEnt.Value);
        }
    }

    // L5 - made generic
    private void OnShutdown<T>(Entity<T> ent, ref ComponentShutdown args) where T : ActionGrantComponent
    {
        foreach (var actionEnt in ent.Comp.ActionEntities)
        {
            _actions.RemoveAction(ent.Owner, actionEnt);
        }
    }
}
