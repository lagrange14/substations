using Content.Shared._L5.CCVar;
using Content.Shared.Weapons.Ranged;
using Robust.Shared.Configuration;
using Robust.Shared.Player;

namespace Content.Shared._L5.Traits.HardOfHearing;

public abstract class SharedSignLanguageSystem : EntitySystem
{
    [Dependency] private readonly IConfigurationManager _config = default!;
    [Dependency] private readonly ISharedPlayerManager _playerManager = default!;

    public bool CanSign()
    {
        if (_config.GetCVar(L5CCVars.AllCanSign))
            return true;

        return CompOrNull<SignLanguageComponent>(_playerManager.LocalSession?.AttachedEntity) != null;
    }
}
