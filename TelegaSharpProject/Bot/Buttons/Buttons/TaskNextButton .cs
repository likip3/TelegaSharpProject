using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Вперёд", "tasknext")]
public class TaskNextButton : Button
{
    public TaskNextButton(Lazy<IMessageService> messageServiceFactory) : base(messageServiceFactory) { }
    
    internal override async Task ExecuteAsync(CallbackQuery ctx)
    {
        MessageServiceFactory.Value.ShowLoadingAsync(ctx);
        
        await MessageServiceFactory.Value.TaskAnotherPageAsync(ctx.From, ctx.Message.Chat, 1);
    }
}