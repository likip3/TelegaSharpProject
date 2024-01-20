using TelegaSharpProject.Application.Bot.Buttons.Base;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Отправить задачу", "sendtask")]
public class SendTaskButton : ButtonBase
{
    private readonly Lazy<IChatManager> _chatManagerFactory;
    public SendTaskButton(Lazy<ITelegramBotClient> botClient, Lazy<IChatManager> chatManagerFactory) : base(botClient)
    {
        _chatManagerFactory = chatManagerFactory;
    }
    
    internal override async Task Execute(CallbackQuery ctx)
    {
        if (!_chatManagerFactory.Value.TryGetChat(ctx.Message.Chat.Id, out var chat))
            return;
        
        chat.SetToInputState();
        
        await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
        await BotClient.Value.SendTextMessageAsync(
            ctx.Message.Chat.Id,
            MessageBuilder.SendTask()
        );
    }
}