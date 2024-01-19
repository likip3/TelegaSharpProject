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
    [SolverButton("Назад", "taskback")]
    public class TaskBackButton : ButtonBase
    {
        internal override async void Execute(CallbackQuery ctx)
        {
            await Bot.Value.GetClient().AnswerCallbackQueryAsync(ctx.Id);
            await SolverChat.GetSolverChat(ctx).BackPageTasks(ctx);
        }

        public TaskBackButton(Lazy<SolverBot> bot) : base(bot)
        {
        }
    }
}
