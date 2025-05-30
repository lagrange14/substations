using Robust.Shared.Audio;

namespace Content.Shared._L5.Traits.HUD
{
    [RegisterComponent]
    public sealed partial class ToggleSecHudComponent : ToggleComponent
    {
        [DataField]
        public override SoundSpecifier? ToggleOnSound { get; set; } = new SoundPathSpecifier("/Audio/Weapons/Guns/Empty/empty.ogg");
        [DataField]
        public override SoundSpecifier? ToggleOffSound { get; set; } = new SoundPathSpecifier("/Audio/Weapons/Guns/Empty/empty.ogg");
    }
}
