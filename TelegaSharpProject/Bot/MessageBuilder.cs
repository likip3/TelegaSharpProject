using TelegaSharpProject.Application.Bot.Buttons.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot
{
    internal class MessageBuilder
    {
        private readonly IButtonManager _buttonManager;
        public MessageBuilder(IButtonManager buttonManager)
        {
            _buttonManager = buttonManager;
        }
        
        public static string GetUserProfile(User user)
        {
            var userScore = 10;
            var userTasks = 2;

            return $"Это ваш профиль, {user.FirstName}\n" +
                   $"У вас {userScore} очков\n" +
                   $"И {userTasks} выполненые задачи";
        }

        public static string GetLeaderBoard()
        {
            return $"Первый Биба\n" +
                   $"Второй Боба";
        }

        public static string GetTasks(int pageNum)
        {
            var title = $"Страница {pageNum}\n";
            if(pageNum == 1)
                return title + "У бобы были 2 рубля, сколько у Бибы сейчас рублей, если он только что вышел из банка Бобы";
            else
                return title + "а не";
        }
        public static IReplyMarkup GetTasksMarkup()
        {
            //todo 
            var inlineButtons = new InlineKeyboardMarkup(
                // new List<InlineKeyboardButton[]>
                // {
                //     new InlineKeyboardButton[]
                //     {
                //         SolverBot.buttonsDict["taskback"],
                //     SolverBot.buttonsDict["title"],
                //     SolverBot.buttonsDict["tasknext"],
                //     },
                //     new InlineKeyboardButton[]
                //     {
                //         SolverBot.buttonsDict["sorttype"],
                //     },
                // }
                Enumerable.Empty<InlineKeyboardButton>()
                );

            return inlineButtons;
        }


        public static string SendTask()
        {
            return "Напишите вашу задачу ниже";
        }

        public static string GetMyTasks(User user)
        {
            return $"К сожелению {user.FirstName} — ленивый)";
        }

        public static string GetAboba()
        {
            return "Aboba";
        }
    }
}
