using Content.Shared.Overlays;

namespace Content.Shared._L5.Traits.HUD
{
    public sealed class ToggleMedHudSystem() : ToggleSystem<ToggleMedHudComponent>
    {
        protected override void TryUpdate(Entity<ToggleMedHudComponent> entity)
        {
            TryUpdateComp<ShowHealthBarsComponent>(entity);
            TryUpdateComp<ShowHealthIconsComponent>(entity);
        }
    }
}
