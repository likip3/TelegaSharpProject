using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot.Buttons.Abstracts;

public abstract class Button
{
    public SolverButton SolverButton { get; }
    protected Lazy<IMessageService> MessageServiceFactory { get; }

    internal abstract Task ExecuteAsync(CallbackQuery ctx);

    internal Button(Lazy<IMessageService> messageServiceFactory)
    {
        MessageServiceFactory = messageServiceFactory;

        var attributes = GetType().GetCustomAttributes(typeof(SolverButton), true);
        if (attributes.Length > 0)
        {
            var solverButton = attributes[0] as SolverButton;
            SolverButton = solverButton;
        }
        else
        {
            throw new Exception("No Attribute");
        }
    }

    public static implicit operator InlineKeyboardButton(Button button)
    {
        return InlineKeyboardButton.WithCallbackData(button.SolverButton.Text, button.SolverButton.Data);
    }
}