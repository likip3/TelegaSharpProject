using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons;

[SolverButton("Мои задачи", "mytasks")]
public class MyTasksButton : ButtonBase
{
    public MyTasksButton(Lazy<SolverBot> bot) : base(bot)
    {
    }
        
    internal override async void Execute(CallbackQuery ctx)
    {
        await Bot.Value.GetClient().AnswerCallbackQueryAsync(ctx.Id);
        await Bot.Value.GetClient().SendTextMessageAsync(
            ctx.Message.Chat,
            MessageBuilder.GetMyTasks(ctx.From)
        );
    }
}