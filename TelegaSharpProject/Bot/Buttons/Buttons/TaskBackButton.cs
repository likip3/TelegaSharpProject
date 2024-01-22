using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Назад", "taskback")]
public class TaskBackButton : Button
{ 
    public TaskBackButton(Lazy<IMessageService> messageServiceFactory) : base(messageServiceFactory) { }
    
    internal override async Task ExecuteAsync(CallbackQuery ctx)
    {
        MessageServiceFactory.Value.ShowLoadingAsync(ctx);
        
        // if (!ChatManagerFactory.Value.TryGetChat(ctx.Message.Chat.Id, out var chat))
        //     return;
        //
        // chat.TrySetPage(1);
        //
        // if (!chat.TrySetPage(chat.PageNum - 1)) return;
        //     
        // await BotClient.Value.EditMessageTextAsync(
        //     chat.Chat,
        //     ctx.Message.MessageId,
        //     MessageBuilder1.GetTasks(chat.PageNum),
        //     replyMarkup: (InlineKeyboardMarkup)MessageBuilder1.GetTasksMarkup()
        // );
    }
}