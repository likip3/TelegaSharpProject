using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Профиль", "profile")]
public class ProfileButton : Button
{
    public ProfileButton(
        Lazy<ITelegramBotClient> botClient,
        IMessageBuilder messageBuilder) : base(botClient, messageBuilder) { }
         
    internal override async Task Execute(CallbackQuery ctx)
    {
        await BotClient.Value.AnswerCallbackQueryAsync(
            ctx.Id, 
            "Загружаем данные");
        
        await BotClient.Value.SendTextMessageAsync(
            ctx.Message.Chat,
            await _messageBuilder.GetUserProfile(ctx.From)
        );
    }
}