using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Посмотреть ответы", "answers")]
public class AnswersButton : Button
{
    private readonly IChatManager _chatManager;
    
    public AnswersButton(Lazy<IMessageService> messageServiceFactory, IChatManager chatManager) : base(messageServiceFactory)
    {
        _chatManager = chatManager;
    }

    internal override async Task ExecuteAsync(CallbackQuery ctx)
    {
        MessageServiceFactory.Value.ShowLoadingAsync(ctx);
        
        var chat = _chatManager.GetChat(ctx.Message.Chat.Id);
        chat.AnswerChatInfo.SetFrom(From.Others);
        chat.SetToAnswerState();

        await MessageServiceFactory.Value.AnswerFirstPageAsync(ctx.From, ctx.Message.Chat);
    }
}