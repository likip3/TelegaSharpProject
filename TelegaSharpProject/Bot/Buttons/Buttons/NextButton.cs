using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Вперёд", "tasknext")]
public class NextButton : Button
{
    private readonly IChatManager _chatManager;

    public NextButton(
        Lazy<IMessageService> messageServiceFactory,
        IChatManager chatManager) : base(messageServiceFactory)
    {
        _chatManager = chatManager;
    }
    
    internal override async Task ExecuteAsync(CallbackQuery ctx)
    {
        MessageServiceFactory.Value.ShowLoadingAsync(ctx);
        
        var chat = _chatManager.GetChat(ctx.Message.Chat.Id);
        
        if (chat.ChatState == ChatState.TaskState)
            await MessageServiceFactory.Value.AnotherPageTaskAsync(ctx.From, ctx.Message.Chat, 1);
        else if (chat.ChatState == ChatState.AnswerState)
            await MessageServiceFactory.Value.AnotherPageAnswerAsync(ctx.From, ctx.Message.Chat, 1);
    }
}