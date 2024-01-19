using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons;

[SolverButton("???", "aboba")]
public class AbobaButton : ButtonBase
{
    public AbobaButton(Lazy<SolverBot> botClient) : base(botClient)
    {
    }
        
    internal override async void Execute(CallbackQuery ctx)
    {
        await BotClient.Value.GetClient().AnswerCallbackQueryAsync(ctx.Id);
        await BotClient.Value.GetClient().SendTextMessageAsync(
            ctx.Message.Chat,
            MessageBuilder.GetAboba()
        );
    }
}