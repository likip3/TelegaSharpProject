using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;
using static TelegaSharpProject.Application.Bot.SolverChat;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Отправить задачу", "sendtask")]
    internal class SendTaskButton : ButtonBase
    {
        internal override async void Execute(CallbackQuery ctx)
        {
            await bot.AnswerCallbackQueryAsync(ctx.Id);
            await bot.SendTextMessageAsync(
            ctx.Message.Chat.Id,
                MessageBuilder.SendTask()
            );
            GetSolverChat(ctx).chatState = ChatState.WaitForInput;
        }
    }
}
