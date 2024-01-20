using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("???", "aboba")]
public class AbobaButton : ButtonBase
{
    public AbobaButton(Lazy<ITelegramBotClient> botClient) : base(botClient)
    { }
        
    internal override async Task Execute(CallbackQuery ctx)
    {
        await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
        
        await BotClient.Value.SendTextMessageAsync(
            ctx.Message.Chat,
            MessageBuilder.GetAboba()
        );
    }
}