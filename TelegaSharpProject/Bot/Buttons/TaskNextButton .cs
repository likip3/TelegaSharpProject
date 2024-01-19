using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Вперёд", "tasknext")]
    public class TaskNextButton : ButtonBase
    {
        public TaskNextButton(Lazy<ITelegramBotClient> botClient) : base(botClient) { }
        internal override async void Execute(CallbackQuery ctx)
        {
            await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
            await SolverChat.GetSolverChat(ctx).NextPageTasks(ctx);
        }
    }
}
