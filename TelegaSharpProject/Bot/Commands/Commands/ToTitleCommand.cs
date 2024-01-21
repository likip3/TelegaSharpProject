using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[SolverCommand("/title")]
public class ToTitleCommand : Command
{
    private readonly IChatManager _chatManager;
    
    public ToTitleCommand(
        IChatManager chatManager,
        IMessageBuilder messageBuilder) : base(messageBuilder)
    {
        _chatManager = chatManager;
    }
    
    public override async void Execute(Message message)
    {
        if (_chatManager.TryGetChat(message.Chat.Id, out var chat))
        {
            //todo write in db
            
            await chat.ToTitle();
        }
        else
            throw new KeyNotFoundException($"Не найден чат пользователя {message.From.Username}");
    }
}