﻿using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Сортировка", "sorttype")]
    public class SortTypeButton : ButtonBase
    {
        public SortTypeButton(Lazy<ITelegramBotClient> botClient) : base(botClient) { }
        
        internal override async void Execute(CallbackQuery ctx)
        {
            await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
            //todo
        }
    }
}
