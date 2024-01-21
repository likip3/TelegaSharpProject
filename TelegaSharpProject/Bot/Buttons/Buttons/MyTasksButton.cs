﻿using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Мои задачи", "mytasks")]
public class MyTasksButton : Button
{
    public MyTasksButton(Lazy<ITelegramBotClient> botClient) : base(botClient) { }

    internal override async Task Execute(CallbackQuery ctx)
    {
        await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
        await BotClient.Value.SendTextMessageAsync(
            ctx.Message.Chat,
            MessageBuilder.GetMyTasks(ctx.From)
        );
    }
}