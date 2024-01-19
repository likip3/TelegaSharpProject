using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Назад", "taskback")]
    public class TaskBackButton : ButtonBase
    {
        public TaskBackButton(Lazy<ITelegramBotClient> botClient) : base(botClient) { }
        internal override async void Execute(CallbackQuery ctx)
        {
            await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
            await SolverChat.GetSolverChat(ctx).BackPageTasks(ctx);
        }
    }
}
