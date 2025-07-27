using Content.Shared._L5.Traits.HUD;

namespace Content.Shared.Actions;

public sealed partial class ActionGrantSystem
{
    private void InitializeL5()
    {
        SubComponent<SecHudTraitComponent>();
        SubComponent<MedHudTraitComponent>();
        SubComponent<BeerHudTraitComponent>();
    }

    // Oop moment.
    // When https://github.com/space-wizards/RobustToolbox/issues/5434 is
    // resolved this needs to switch to using that. Please. I beg you.
    private void SubComponent<T>() where T : ActionGrantComponent
    {
        SubscribeLocalEvent<T, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<T, ComponentShutdown>(OnShutdown);
    }
}
