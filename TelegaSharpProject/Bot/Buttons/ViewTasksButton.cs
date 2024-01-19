using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons;

[SolverButton("Просмотр задач", "viewtasks")]
public class ViewTasksButton : ButtonBase
{
    public ViewTasksButton(Lazy<ITelegramBotClient> botClient) : base(botClient) { }
    
    internal override async void Execute(CallbackQuery ctx)
    {
        var chat = SolverChat.GetSolverChat(ctx);
        chat.SetPage(1);
        await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
        await BotClient.Value.SendTextMessageAsync(
            chat.chat,
            MessageBuilder.GetTasks(chat.PageNum),
            replyMarkup: MessageBuilder.GetTasksMarkup()
        );
    }
}