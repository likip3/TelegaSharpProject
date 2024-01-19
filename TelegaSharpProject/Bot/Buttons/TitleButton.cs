﻿using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Главная","title")]
    public class TitleButton : ButtonBase
    {
        public TitleButton(Lazy<ITelegramBotClient> botClient) : base(botClient) { }
            
        internal override async void Execute(CallbackQuery ctx)
        {
            await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
            SolverChat.GetSolverChat(ctx).ToTitle();
        }
    }
}
