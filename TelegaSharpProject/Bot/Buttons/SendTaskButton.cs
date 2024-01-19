using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;
using static TelegaSharpProject.Application.Bot.SolverChat;

namespace TelegaSharpProject.Application.Bot.Buttons;

[SolverButton("Отправить задачу", "sendtask")]
public class SendTaskButton : ButtonBase
{
    public SendTaskButton(Lazy<SolverBot> bot) : base(bot)
    {
    }
        
    internal override async void Execute(CallbackQuery ctx)
    {
        await Bot.Value.GetClient().AnswerCallbackQueryAsync(ctx.Id);
        await Bot.Value.GetClient().SendTextMessageAsync(
            ctx.Message.Chat.Id,
            MessageBuilder.SendTask()
        );
        GetSolverChat(ctx).chatState = ChatState.WaitForInput;
    }
}