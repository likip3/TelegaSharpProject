using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Мои задачи", "mytasks")]
public class MyTasksButton : Button
{
    private readonly IChatManager _chatManager;
    public MyTasksButton(Lazy<IMessageService> messageServiceFactory, IChatManager chatManager) : base(messageServiceFactory)
    {
        _chatManager = chatManager;
    }

    internal override async Task ExecuteAsync(CallbackQuery ctx)
    {
        MessageServiceFactory.Value.ShowLoadingAsync(ctx);
        
        var chat = _chatManager.GetChat(ctx.Message.Chat.Id);
        chat.TaskChatInfo.SetFrom(From.Me);

        await MessageServiceFactory.Value.TaskFirstPageAsync(ctx.From, ctx.Message.Chat);
    }
}