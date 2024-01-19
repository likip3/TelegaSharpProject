﻿using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Просмотр задач", "viewtasks")]
    internal class ViewTasksButton : ButtonBase
    {
        internal override async void Execute(CallbackQuery ctx)
        {
            var chat = SolverChat.GetSolverChat(ctx);
            chat.SetPage(1);
            await chat.SendTasksList(ctx);
        }
    }
}