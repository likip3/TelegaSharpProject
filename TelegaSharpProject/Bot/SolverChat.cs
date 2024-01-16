using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot
{
    internal class SolverChat
    {
        internal enum ChatState
        {
            None,
            WaitForCommand,
            WaitForInput,
        }

        private static Dictionary<long, SolverChat> chats = new();

        internal ChatState chatState;

        private Chat chat;
        private User user;

        private ITelegramBotClient bot;

        private int pageNum;

        public SolverChat(Chat chat, User user)
        {
            this.chat = chat;
            this.user = user;
            chatState = ChatState.WaitForCommand;
            bot = SolverBot.botClient;
        }


        public async void ToTitle()
        {
            var inlineButtons = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Профиль", "b1"),
                        InlineKeyboardButton.WithCallbackData("Таблица лидеров", "b2"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Просмотр задач", "b3"),
                        InlineKeyboardButton.WithCallbackData("Отправить задачу", "b4"),
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Мои задачи", "b5"),
                    InlineKeyboardButton.WithCallbackData("???", "b6"),
                }
                });
            await bot.SendTextMessageAsync(
                chat.Id,
                "Что вы хотите?",
                replyMarkup: inlineButtons
            );
            chatState = ChatState.WaitForCommand;
        }

        public async void SendInput(Message message)
        {
            if(chatState !=  ChatState.WaitForInput) return;
            chatState = ChatState.WaitForCommand;

            //todo запись сообщения

            await bot.SendTextMessageAsync(
                chat.Id,
                "Записал!"
            );
            ToTitle();
            Console.WriteLine($"{message.From.FirstName} ({message.From.Id}) отправил задачу:\n{message.Text}");
        }

        public async void ButtonPressed(CallbackQuery callbackQuery)
        {
            chatState = ChatState.WaitForCommand;
            switch (callbackQuery.Data)
            {
                case "toTitle":
                    await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
                    ToTitle();
                    break;
                case "b1":
                    await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
                    await bot.SendTextMessageAsync(
                        chat.Id,
                        MessageBuilder.GetUserProfile(callbackQuery.From)
                    );
                    break;
                case "b2":
                    await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
                    await bot.SendTextMessageAsync(
                        chat.Id,
                        MessageBuilder.GetLeaderBoard()
                    );
                    break;
                case "b3":
                    pageNum = 1;
                    await SendTasksList(callbackQuery);
                    break;
                case "b4":
                    await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
                    await bot.SendTextMessageAsync(
                        chat.Id,
                        MessageBuilder.SendTask()
                    );
                    chatState = ChatState.WaitForInput;
                    break;
                case "b5":
                    await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
                    await bot.SendTextMessageAsync(
                        chat.Id,
                        MessageBuilder.GetMyTasks(callbackQuery.From)
                    );
                    break;
                case "b6":
                    await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
                    await bot.SendTextMessageAsync(
                        chat.Id,
                        MessageBuilder.GetAboba()
                    );
                    break;
                case "taskBack":
                    await BackPageTasks(callbackQuery);
                    break;
                case "taskNext":
                    await NextPageTasks(callbackQuery);
                    break;
            }
        }

        private async Task NextPageTasks(CallbackQuery callbackQuery)
        {
            await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
            pageNum++;
            await bot.EditMessageTextAsync(
                chat.Id,
                callbackQuery.Message.MessageId,
                MessageBuilder.GetTasks(pageNum),
                replyMarkup: (InlineKeyboardMarkup)MessageBuilder.GetTasksMarkup()
            );
        }
        private async Task BackPageTasks(CallbackQuery callbackQuery)
        {
            await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
            if (pageNum <= 1) return;
            pageNum--;
            await bot.EditMessageTextAsync(
                chat.Id,
                callbackQuery.Message.MessageId,
                MessageBuilder.GetTasks(pageNum),
                replyMarkup: (InlineKeyboardMarkup)MessageBuilder.GetTasksMarkup()
            );
        }



        private async Task SendTasksList(CallbackQuery callbackQuery)
        {
            await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
            await bot.SendTextMessageAsync(
                chat.Id,
                MessageBuilder.GetTasks(pageNum),
                replyMarkup: MessageBuilder.GetTasksMarkup()
            );
        }

        public static SolverChat GetSolverChat(Message message)
        {
            if (chats.TryGetValue(message.Chat.Id, out var solverChat))
                return solverChat;
            var chat = new SolverChat(message.Chat, message.From);
            chats.Add(message.Chat.Id,chat);
            return chat;
        }

        public static SolverChat GetSolverChat(CallbackQuery callback) => GetSolverChat(callback.Message);

    }
}
