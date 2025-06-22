using Content.Server.Administration;
using Content.Server.GameTicking.Presets;
using Content.Shared.Administration;
using Content.Shared.CCVar;
using Robust.Shared.Configuration;
using Robust.Shared.Console;
using Robust.Shared.Timing;

namespace Content.Server.GameTicking.Commands
{
    [AdminCommand(AdminFlags.Round)]
    public sealed class GoLobbyCommand : IConsoleCommand
    {
        [Dependency] private readonly IEntityManager _e = default!;
        [Dependency] private readonly IGameTiming _time = default!; // L5

        public string Command => "golobby";
        public string Description => "Enables the lobby and restarts the round.";
        // L5 - add confirm
        public string Help => $"Usage: {Command} [confirm] / {Command} <preset> [confirm]";

        public void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            GamePresetPrototype? preset = null;
            var presetName = string.Join(" ", args);
            // Begin L5 changes - add confirm
            var confirm = presetName.EndsWith("confirm");
            if (confirm)
                presetName = presetName[..^"confirm".Length].TrimEnd();
            else if (_time.RealTime > TimeSpan.FromHours(1))
            {
                shell.WriteLine($"Add 'confirm' to the command to really end the round and go back to the lobby.");
                return;
            }
            // End L5 changes

            var ticker = _e.System<GameTicker>();

            if (presetName.Length > 0) // L5 - was args.Length
            {
                if (!ticker.TryFindGamePreset(presetName, out preset))
                {
                    shell.WriteLine($"No preset found with name {presetName}");
                    return;
                }
            }

            var config = IoCManager.Resolve<IConfigurationManager>();
            config.SetCVar(CCVars.GameLobbyEnabled, true);

            ticker.RestartRound();

            if (preset != null)
            {
                ticker.SetGamePreset(preset);
            }

            shell.WriteLine($"Enabling the lobby and restarting the round.{(preset == null ? "" : $"\nPreset set to {presetName}")}");
        }
    }
}
