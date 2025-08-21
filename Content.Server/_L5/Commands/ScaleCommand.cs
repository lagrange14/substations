using Content.Server.Administration;
using Content.Shared.Administration;
using Content.Shared.Sprite;
using Robust.Shared.Console;
using Robust.Shared.Physics;
using Robust.Shared.Physics.Systems;

namespace Content.Server._L5.Commands;

[AdminCommand(AdminFlags.Fun)]
public sealed class ScaleCommand : LocalizedEntityCommands
{
    [Dependency] private readonly SharedScaleVisualsSystem _scaleVis = default!;
    [Dependency] private readonly SharedPhysicsSystem _physics = default!;

    public override string Command => "scale";

    public override void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (args.Length != 2)
        {
            shell.WriteError(Loc.GetString("shell-wrong-arguments-number-need-specific",
                ("properAmount", 2),
                ("currentAmount", args.Length)));
            return;
        }

        if (!NetEntity.TryParse(args[0], out var netEntity))
        {
            shell.WriteError(Loc.GetString("shell-invalid-entity-id"));
            return;
        }

        if (!float.TryParse(args[1], out var scale))
        {
            shell.WriteError(Loc.GetString("shell-argument-must-be-number"));
            return;
        }

        if (scale <= 0)
        {
            shell.WriteError(Loc.GetString("shell-argument-must-be-positive"));
            return;
        }

        var ent = EntityManager.GetEntity(netEntity);
        var scaleVec = _scaleVis.GetSpriteScale(ent) * scale;
        _scaleVis.SetSpriteScale(ent, scaleVec);
        if (!EntityManager.TryGetComponent<FixturesComponent>(ent, out var fix))
            return;

        _physics.ScaleFixtures((ent, fix), scale);
    }

    public override CompletionResult GetCompletion(IConsoleShell shell, string[] args)
    {
        return args.Length switch
        {
            1 => CompletionResult.FromOptions(CompletionHelper.NetEntities(args[0], EntityManager)),
            2 => CompletionResult.FromHint(Loc.GetString("cmd-hint-float")),
            _ => CompletionResult.Empty,
        };
    }
}
