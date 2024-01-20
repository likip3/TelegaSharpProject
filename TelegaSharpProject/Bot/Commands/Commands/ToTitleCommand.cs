using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using TelegaSharpProject.Application.Bot.Commands.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[Attributes.SolverCommand("/title")]
public class ToTitleCommand : ICommand
{
    private readonly IChatManager _chatManager;
    
    public ToTitleCommand(IChatManager chatManager)
    {
        _chatManager = chatManager;
    }
    
    public async void Execute(Message message)
    {
        if (_chatManager.TryGetChat(message.Chat.Id, out var chat))
        {
            if (chat.ChatState != ChatState.WaitForInput)
                return;
            
            //todo write in db
            
            await chat.ToTitle();
        }
        else
            throw new KeyNotFoundException($"Не найден чат пользователя {message.From.Username}");
    }
}