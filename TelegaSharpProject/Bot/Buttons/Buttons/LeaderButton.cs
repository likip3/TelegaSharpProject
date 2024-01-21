using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Таблица лидеров","leaders")]
public class LeaderButton : Button
{
    public LeaderButton(
        Lazy<ITelegramBotClient> botClient,
        IMessageBuilder messageBuilder) : base(botClient, messageBuilder)
    { }
        
    internal override async Task Execute(CallbackQuery ctx)
    {
        ShowLoading(ctx);
        
        await BotClient.Value.SendTextMessageAsync(
            ctx.Message.Chat.Id,
            await Builder.GetLeaderBoard()
        );
    }
}