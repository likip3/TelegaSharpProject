using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Таблица лидеров","leaders")]
    public class LeaderButton : ButtonBase
    {
        public LeaderButton(Lazy<SolverBot> botClient) : base(botClient)
        { }
        
        internal override async void Execute(CallbackQuery ctx)
        {
            await BotClient.Value.GetClient().AnswerCallbackQueryAsync(ctx.Id);
            await BotClient.Value.GetClient().SendTextMessageAsync(
                ctx.Message.Chat.Id,
                MessageBuilder.GetLeaderBoard()
            );
        }
    }
}
