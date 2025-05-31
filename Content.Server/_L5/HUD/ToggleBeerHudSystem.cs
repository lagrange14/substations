using Content.Shared.Chemistry.Components;
using Content.Shared.Overlays;

namespace Content.Shared._L5.Traits.HUD
{
    public sealed class ToggleBeerHudSystem() : ToggleSystem<ToggleBeerHudComponent>
    {
        protected override void TryUpdate(Entity<ToggleBeerHudComponent> entity)
        {
            TryUpdateComp<ShowThirstIconsComponent>(entity);
            TryUpdateComp<SolutionScannerComponent>(entity);
        }
    }
}
