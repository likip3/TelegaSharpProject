using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.Commands.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[Attributes.SolverCommand("/start")]
public class StartCommand : ICommand
{
    private readonly IChatManager _chatManager;
    
    public StartCommand(IChatManager chatManager)
    {
        _chatManager = chatManager;
    }
    
    public void Execute(Message message)
    {
        _chatManager.StartChat(message.Chat);
    }
}