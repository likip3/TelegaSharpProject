using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons;

[SolverButton("???", "aboba")]
public class AbobaButton : ButtonBase
{
    public AbobaButton(Lazy<SolverBot> bot) : base(bot)
    {
    }
        
    internal override async void Execute(CallbackQuery ctx)
    {
        await Bot.Value.GetClient().AnswerCallbackQueryAsync(ctx.Id);
        await Bot.Value.GetClient().SendTextMessageAsync(
            ctx.Message.Chat,
            MessageBuilder.GetAboba()
        );
    }
}