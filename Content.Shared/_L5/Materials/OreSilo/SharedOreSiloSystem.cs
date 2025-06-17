using Content.Shared.Lathe;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Materials.OreSilo;

public abstract partial class SharedOreSiloSystem
{
    [Dependency] private readonly IComponentFactory _factory = default!;
    [Dependency] private readonly IPrototypeManager _prototype = default!;

    private void OnLatheFinishPrinting(Entity<OreSiloClientComponent> ent, ref LatheFinishPrintingEvent args)
    {
        if (args.Handled)
            return;

        if (!TryComp<OreSiloClientComponent>(ent, out var client)
            || !client.Enabled)
            return;

        if (client.Silo is not { } silo
            || !TryComp<MaterialStorageComponent>(silo, out var siloStorage))
            return;

        if (!CanTransmitMaterials(silo, ent))
            return;

        var latheComp = args.Lathe.Comp;
        if (latheComp.CurrentRecipe is { Result: { } resultProtoId }
            && _prototype.TryIndex(resultProtoId, out var resultProto)
            && resultProto.TryGetComponent<PhysicalCompositionComponent>(out var composition, _factory))
        {
            args.Handled = _materialStorage.TryChangeMaterialAmount((silo, siloStorage), composition.MaterialComposition);
        }
    }

    private void OnSiloButtonPressed(Entity<OreSiloClientComponent> ent, ref EnableSiloButtonPressed args)
    {
        if (!ent.Comp.Source)
            return;
        ent.Comp.Enabled = !ent.Comp.Enabled;
        Dirty(ent);
    }
}

[ByRefEvent]
public record struct LatheFinishPrintingEvent(Entity<LatheComponent> Lathe, bool Handled);

[Serializable, NetSerializable]
public sealed class EnableSiloButtonPressed(bool enabled) : BoundUserInterfaceMessage
{
    public bool Enabled { get; } = enabled;
}
