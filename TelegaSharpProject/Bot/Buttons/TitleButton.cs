using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Главная","title")]
    public class TitleButton : ButtonBase
    {
        internal override async void Execute(CallbackQuery ctx)
        {
            await Bot.Value.GetClient().AnswerCallbackQueryAsync(ctx.Id);
            SolverChat.GetSolverChat(ctx).ToTitle();
        }

        public TitleButton(Lazy<SolverBot> bot) : base(bot)
        {
        }
    }
}
