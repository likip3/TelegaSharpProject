using System;
using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Профиль", "profile")]
    internal class ProfileButton : ButtonBase
    {
        internal override async void Execute(CallbackQuery ctx)
        {
            await bot.AnswerCallbackQueryAsync(ctx.Id);
            await bot.SendTextMessageAsync(
                ctx.Message.Chat,
                MessageBuilder.GetUserProfile(ctx.From)
            );
        }
    }
}
