using TelegaSharpProject.Application.Bot.Buttons.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot
{
    internal class MessageBuilder1
    {
        private readonly IButtonManager _buttonManager;
        public MessageBuilder1(IButtonManager buttonManager)
        {
            _buttonManager = buttonManager;
        }

        public static string GetTasks(int pageNum)
        {
            var title = $"Страница {pageNum}\n";
            if(pageNum == 1)
                return title + "У бобы были 2 рубля, сколько у Бибы сейчас рублей, если он только что вышел из банка Бобы";
            
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
