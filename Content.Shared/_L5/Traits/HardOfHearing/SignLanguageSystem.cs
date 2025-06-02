using Content.Shared._L5.CCVar;
using Robust.Shared.Configuration;

namespace Content.Shared._L5.Traits.HardOfHearing;

public sealed class SignLanguageSystem : EntitySystem
{
    [Dependency] private readonly IConfigurationManager _config = default!;

    private bool _canAllSign;

    public override void Initialize()
    {
        base.Initialize();

        Subs.CVar(_config, L5CCVars.AllCanSign, canAllSign => _canAllSign = canAllSign, true);
    }

    public bool CanSign(Entity<SignLanguageComponent?> ent)
    {
        return _canAllSign || HasComp<SignLanguageComponent>(ent);
    }
}
