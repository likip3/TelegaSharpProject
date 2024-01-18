using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands
{
    public class CommandModule
    {
        [Command("/title", new[] { "главная","/start" })]
        public void ToTitle(Message ctx)
        {
            SolverChat.GetSolverChat(ctx).ToTitle();
        }

        [Command("/ab")]
        public void Abobus(Message ctx)
        {
            SolverBot.botClient.SendTextMessageAsync(ctx.Chat.Id, "Сам такой");
        }
    }
}
