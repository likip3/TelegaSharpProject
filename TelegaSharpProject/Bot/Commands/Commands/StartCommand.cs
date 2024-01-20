using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[SolverCommand("/start")]
public class StartCommand : Command
{
    private readonly IChatManager _chatManager;
    
    public StartCommand(IChatManager chatManager)
    {
        _chatManager = chatManager;
    }
    
    public override async void Execute(Message message)
    {
        _chatManager.StartChat(message.Chat);

        if (_chatManager.TryGetChat(message.Chat.Id, out var chat))
            await chat.ToTitle();
    }
}