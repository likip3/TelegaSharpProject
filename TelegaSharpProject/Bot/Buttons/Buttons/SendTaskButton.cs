using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Отправить задачу", "sendtask")]
public class SendTaskButton : Button
{
    private readonly Lazy<IChatManager> _chatManagerFactory;
    public SendTaskButton(
        Lazy<ITelegramBotClient> botClient,
        IMessageBuilder messageBuilder,
        Lazy<IChatManager> chatManagerFactory) : base(botClient, messageBuilder)
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
            MessageBuilder1.SendTask()
        );
    }
}