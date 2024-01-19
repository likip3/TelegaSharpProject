using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons;

[SolverButton("Просмотр задач", "viewtasks")]
public class ViewTasksButton : ButtonBase
{
    internal override async void Execute(CallbackQuery ctx)
    {
        var chat = SolverChat.GetSolverChat(ctx);
        chat.SetPage(1);
        await chat.SendTasksList(ctx);
    }

    public ViewTasksButton(Lazy<SolverBot> bot) : base(bot)
    {
    }
}