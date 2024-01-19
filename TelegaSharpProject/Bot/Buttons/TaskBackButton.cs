using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Назад", "taskback")]
    public class TaskBackButton : ButtonBase
    {
        public TaskBackButton(Lazy<ITelegramBotClient> botClient) : base(botClient) { }
        internal override async void Execute(CallbackQuery ctx)
        {
            await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
            var chat = SolverChat.GetSolverChat(ctx);

            if (!chat.SetPage(chat.PageNum - 1)) return;
            
            await BotClient.Value.EditMessageTextAsync(
                chat.chat,
                ctx.Message.MessageId,
                MessageBuilder.GetTasks(chat.PageNum),
                replyMarkup: (InlineKeyboardMarkup)MessageBuilder.GetTasksMarkup()
            );
        }
    }
}
