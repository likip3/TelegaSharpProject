using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons;

[SolverButton("Мои задачи", "mytasks")]
public class MyTasksButton : ButtonBase
{
    public MyTasksButton(Lazy<SolverBot> botClient) : base(botClient)
    {
    }
        
    internal override async void Execute(CallbackQuery ctx)
    {
        await BotClient.Value.GetClient().AnswerCallbackQueryAsync(ctx.Id);
        await BotClient.Value.GetClient().SendTextMessageAsync(
            ctx.Message.Chat,
            MessageBuilder.GetMyTasks(ctx.From)
        );
    }
}