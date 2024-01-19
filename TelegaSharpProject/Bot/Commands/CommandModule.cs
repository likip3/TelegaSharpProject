using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands
{
    public class CommandModule
    {
        [SolverCommand("/title", new[] { "главная","/start" })]
        public void ToTitle(Message ctx)
        {
            SolverChat.GetSolverChat(ctx).ToTitle();
        }

        [SolverCommand("/leaders", new[] { "лидеры", })]
        public void ToLeaders(Message ctx)
        {
            SolverBot.BotClient.SendTextMessageAsync(
                ctx.Chat.Id,
                MessageBuilder.GetLeaderBoard()
            );
        }

        [SolverCommand("/mytasks")]
        public void ToMyTasks(Message ctx)
        {
            SolverBot.BotClient.SendTextMessageAsync(
                ctx.Chat.Id,
                MessageBuilder.GetMyTasks(ctx.From)
            );
        }

        [SolverCommand("/tasks")]
        public void ToTasks(Message ctx)
        {
            SolverChat.GetSolverChat(ctx).SetPage(1);
            SolverBot.BotClient.SendTextMessageAsync(
                ctx.Chat.Id,
                MessageBuilder.GetTasks(1),
                replyMarkup: MessageBuilder.GetTasksMarkup()
            );
        }

        [SolverCommand("/ab")]
        public void Abobus(Message ctx)
        {
            SolverBot.BotClient.SendTextMessageAsync(ctx.Chat.Id, "Сам такой");
        }
    }
}
