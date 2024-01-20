using System.Runtime.Serialization;
using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.Commands.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IChatManager _chatManager;
        private readonly Dictionary<string, ICommand> _commands = new();
        private readonly Dictionary<string, ICommand> _commandsNotIndexed = new();
        
        public CommandExecutor(ICommand[] commands, IChatManager chatManager)
        {
            _chatManager = chatManager;
            foreach (var command in commands)
            {
                var attributes = commands
                    .GetType()
                    .GetCustomAttributes(typeof(Attributes.SolverCommand), true);

                if (attributes.Length == 0)
                    throw new SerializationException("No SolverCommand attributes provided");

                var solverCommand = (Attributes.SolverCommand) attributes[0];
                if (!solverCommand.NeedsToCheck)
                    _commandsNotIndexed[solverCommand.Name] = command;
                else
                    _commands[solverCommand.Name] = command;
            }
        }

        public async Task Execute(Message message)
        {
            Console.WriteLine(message.Text);
            if (_commands.TryGetValue(message.Text, out var command))
                command.Execute(message);
            else if (_chatManager.TryGetChat(message.Chat.Id, out var chat) && chat.ChatState == ChatState.WaitForInput)
            {
                _commandsNotIndexed["input"].Execute(message);
                await chat.ToTitle();
            }
        }
    }
}
