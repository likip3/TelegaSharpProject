using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Таблица лидеров","leaders")]
    internal class LeaderButton : ButtonBase
    {
        internal override async void Execute(CallbackQuery ctx)
        {
            await bot.AnswerCallbackQueryAsync(ctx.Id);
            await bot.SendTextMessageAsync(
                ctx.Message.Chat.Id,
                MessageBuilder.GetLeaderBoard()
            );
        }
    }
}
