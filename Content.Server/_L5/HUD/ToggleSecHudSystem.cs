using Content.Shared.Contraband;
using Content.Shared.Overlays;

namespace Content.Shared._L5.Traits.HUD
{
    public sealed class ToggleSecHudSystem() : ToggleSystem<ToggleSecHudComponent>
    {
        protected override void TryUpdate(Entity<ToggleSecHudComponent> entity)
        {
            TryUpdateComp<ShowJobIconsComponent>(entity);
            TryUpdateComp<ShowMindShieldIconsComponent>(entity);
            TryUpdateComp<ShowCriminalRecordIconsComponent>(entity);
            TryUpdateComp<ShowContrabandDetailsComponent>(entity);
        }
    }
}
