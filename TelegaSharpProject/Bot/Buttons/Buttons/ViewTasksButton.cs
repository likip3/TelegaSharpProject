using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Просмотр задач", "viewtasks")]
public class ViewTasksButton : Button
{
    private readonly IChatManager _chatManager;
    public ViewTasksButton(Lazy<IMessageService> messageServiceFactory, IChatManager chatManager) : base(messageServiceFactory)
    {
        _chatManager = chatManager;
    }
    
    internal override async Task ExecuteAsync(CallbackQuery ctx)
    {
        MessageServiceFactory.Value.ShowLoadingAsync(ctx);
        
        var chat = _chatManager.GetChat(ctx.Message.Chat.Id);
        chat.TaskChatInfo.SetTaskFrom(TaskFrom.Others);

        await MessageServiceFactory.Value.TaskFirstPageAsync(ctx.From, ctx.Message.Chat);
    }
}