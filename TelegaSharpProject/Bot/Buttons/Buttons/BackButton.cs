﻿using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Назад", "taskback")]
public class BackButton : Button
{ 
    public BackButton(Lazy<IMessageService> messageServiceFactory) : base(messageServiceFactory) { }
    
    internal override async Task ExecuteAsync(CallbackQuery ctx)
    {
        MessageServiceFactory.Value.ShowLoadingAsync(ctx);

        await MessageServiceFactory.Value.AnotherPageTaskAsync(ctx.From, ctx.Message.Chat, -1);
    }
}