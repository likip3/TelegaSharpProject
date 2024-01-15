using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot
{
    internal static class MessageBuilder
    {
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

        public static string GetTasks()
        {
            return "У бобы были 2 рубля, сколько у Бибы сейчас рублей, если он только что вышел из банка Бобы";
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
