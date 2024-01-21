using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Мои задачи", "mytasks")]
public class MyTasksButton : Button
{
    public MyTasksButton(
        Lazy<ITelegramBotClient> botClient,
        IMessageBuilder messageBuilder) : base(botClient, messageBuilder) { }

    internal override async Task Execute(CallbackQuery ctx)
    {
        await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
        await BotClient.Value.SendTextMessageAsync(
            ctx.Message.Chat,
            MessageBuilder1.GetMyTasks(ctx.From)
        );
    }
}