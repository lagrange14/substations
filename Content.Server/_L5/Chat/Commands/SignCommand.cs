using Content.Server.Chat.Systems;
using Content.Shared.Administration;
using Robust.Shared.Console;
using Robust.Shared.Enums;

namespace Content.Server._L5.Chat.Commands
{
    [AnyCommand]
    internal sealed class SignCommand : IConsoleCommand
    {
        public string Command => "sign";
        public string Description => "Say something in sign language.";
        public string Help => "sign <text>";

        public void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            if (shell.Player is not { } player)
            {
                shell.WriteError("This command cannot be run from the server.");
                return;
            }

            if (player.Status != SessionStatus.InGame)
                return;

            if (player.AttachedEntity is not {} playerEntity)
            {
                shell.WriteError("You don't have an entity!");
                return;
            }

            if (args.Length < 1)
                return;

            var message = string.Join(" ", args).Trim();
            if (string.IsNullOrEmpty(message))
                return;

            IoCManager.Resolve<IEntitySystemManager>().GetEntitySystem<ChatSystem>()
                .TrySendInGameICMessage(playerEntity, message, InGameICChatType.Sign, ChatTransmitRange.NoGhosts, false, shell, player);
        }
    }
}
