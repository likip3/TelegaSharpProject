using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Вперёд", "tasknext")]
    internal class TaskNextButton : ButtonBase
    {
        internal override async void Execute(CallbackQuery ctx)
        {
            await bot.AnswerCallbackQueryAsync(ctx.Id);
            SolverChat.GetSolverChat(ctx).NextPageTasks(ctx);
        }
    }
}
