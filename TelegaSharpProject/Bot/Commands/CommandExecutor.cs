using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands;

public class CommandExecutor : ICommandExecutor
{
    private readonly IChatManager _chatManager;
    private readonly Dictionary<string, Command> _commands = new();
    private readonly Dictionary<string, Command> _commandsNotIndexed = new();
        
    public CommandExecutor(Command[] commands, IChatManager chatManager)
    {
        _chatManager = chatManager;
        foreach (var command in commands)
        {
            if (command.SolverCommand.NeedsIndexing)
                _commands[command.SolverCommand.Name] = command;
            else
                _commandsNotIndexed[command.SolverCommand.Name] = command;
        }
    }

    public async Task Execute(Message message)
    {
        if (_commands.TryGetValue(message.Text, out var command))
            await command.Execute(message);
        else if (_chatManager.TryGetChat(message.Chat.Id, out var chat) && chat.ChatState == ChatState.WaitForInput)
        {
            _commandsNotIndexed["input"].Execute(message);
            chat.Reset();
        }
        else
            chat.Reset();
    }
}