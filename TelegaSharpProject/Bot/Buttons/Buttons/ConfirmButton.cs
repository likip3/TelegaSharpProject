using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Пометить как правильный", "confirm")]
public class ConfirmButton : Button
{
    public ConfirmButton(Lazy<IMessageService> messageServiceFactory) : base(messageServiceFactory)
    {
    }

    internal override async Task ExecuteAsync(CallbackQuery ctx)
    {
        MessageServiceFactory.Value.ShowLoadingAsync(ctx);

        await MessageServiceFactory.Value.ConfirmAnswerAsync(ctx.From, ctx.Message.Chat);
    }
}