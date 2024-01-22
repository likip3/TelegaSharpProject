using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Вперёд", "tasknext")]
public class TaskNextButton : Button
{
    public TaskNextButton(Lazy<IMessageService> messageServiceFactory) : base(messageServiceFactory) { }
    
    internal override async Task ExecuteAsync(CallbackQuery ctx)
    {
        MessageServiceFactory.Value.ShowLoadingAsync(ctx);
        // chat.TrySetPage(1);
        //
        // chat.TrySetPage(chat.PageNum + 1);
        //
        // await BotClient.Value.EditMessageTextAsync(
        //     chat.Chat,
        //     ctx.Message.MessageId,
        //     MessageBuilder1.GetTasks(chat.PageNum),
        //     replyMarkup: (InlineKeyboardMarkup)MessageBuilder1.GetTasksMarkup()
        // );
    }
}