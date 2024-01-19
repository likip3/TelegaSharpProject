using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Таблица лидеров","leaders")]
    public class LeaderButton : ButtonBase
    {
        public LeaderButton(Lazy<SolverBot> bot) : base(bot)
        { }
        
        internal override async void Execute(CallbackQuery ctx)
        {
            await Bot.Value.GetClient().AnswerCallbackQueryAsync(ctx.Id);
            await Bot.Value.GetClient().SendTextMessageAsync(
                ctx.Message.Chat.Id,
                MessageBuilder.GetLeaderBoard()
            );
        }
    }
}
