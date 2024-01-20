using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Таблица лидеров","leaders")]
public class LeaderButton : ButtonBase
{
    public LeaderButton(Lazy<ITelegramBotClient> botClient) : base(botClient)
    { }
        
    internal override async Task Execute(CallbackQuery ctx)
    {
        await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
        await BotClient.Value.SendTextMessageAsync(
            ctx.Message.Chat.Id,
            MessageBuilder.GetLeaderBoard()
        );
    }
}