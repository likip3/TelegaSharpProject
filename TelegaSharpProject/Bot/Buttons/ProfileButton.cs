using System;
using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons;

[SolverButton("Профиль", "profile")]
public class ProfileButton : ButtonBase
{
    public ProfileButton(Lazy<SolverBot> bot) : base(bot) { }
         
    internal override async void Execute(CallbackQuery ctx)
    {
        await Bot.Value.GetClient().AnswerCallbackQueryAsync(ctx.Id);
        await Bot.Value.GetClient().SendTextMessageAsync(
            ctx.Message.Chat,
            MessageBuilder.GetUserProfile(ctx.From)
        );
    }
}