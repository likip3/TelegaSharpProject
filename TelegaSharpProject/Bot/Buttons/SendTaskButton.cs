using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;
using static TelegaSharpProject.Application.Bot.SolverChat;

namespace TelegaSharpProject.Application.Bot.Buttons;

[SolverButton("Отправить задачу", "sendtask")]
public class SendTaskButton : ButtonBase
{
    public SendTaskButton(Lazy<ITelegramBotClient> botClient) : base(botClient) { }
    
    internal override async void Execute(CallbackQuery ctx)
    {
        await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
        await BotClient.Value.SendTextMessageAsync(
            ctx.Message.Chat.Id,
            MessageBuilder.SendTask()
        );
        GetSolverChat(ctx).chatState = ChatState.WaitForInput;
    }
}