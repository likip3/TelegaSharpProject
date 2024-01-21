using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Главная","title")]
public class TitleButton : Button
{
    private readonly Lazy<IChatManager> _chatManagerFactory;
    public TitleButton(
        Lazy<ITelegramBotClient> botClient, 
        IMessageBuilder messageBuilder,
        Lazy<IChatManager> chatManagerFactory) : base(botClient, messageBuilder)
    {
        _chatManagerFactory = chatManagerFactory;
    }
            
    internal override async Task Execute(CallbackQuery ctx)
    {
        await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
        
        if (_chatManagerFactory.Value.TryGetChat(ctx.Message.Chat.Id, out var chat))
            return;

        await chat.ToTitle();
    }
}