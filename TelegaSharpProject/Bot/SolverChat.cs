using System.Reflection;
using System.Security.Cryptography;
using TelegaSharpProject.Application.Bot.Buttons.Base;
using TelegaSharpProject.Application.Bot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot
{
    public class SolverChat
    {
        internal enum ChatState
        {
            None,
            WaitForCommand,
            WaitForInput,
        }

        public enum SortType
        {
            ByTimeUP,
            ByTimeDOWN,
            ByRating,

        }

        private static Dictionary<long, SolverChat> chats = new();

        internal ChatState chatState;

        internal readonly Chat chat;
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

        public bool SetPage(int page)
        {
            if(pageNum == page || page < 1) return false;
            pageNum = page;
            return true;
        }

        public async void ToTitle()
        {
            var inlineButtons = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>
                {
                    new InlineKeyboardButton[]
                    {
                        SolverBot.buttonsDict["profile"],
                        SolverBot.buttonsDict["leaders"]
                    },
                    new InlineKeyboardButton[]
                    {
                        SolverBot.buttonsDict["viewtasks"],
                        SolverBot.buttonsDict["sendtask"]
                    },
                    new InlineKeyboardButton[]
                    {
                        SolverBot.buttonsDict["mytasks"],
                        SolverBot.buttonsDict["aboba"]
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

            //todo запись сообщения в базу

            await bot.SendTextMessageAsync(
                chat.Id,
                "Записал!"
            );
            ToTitle();
            Console.WriteLine($"{message.From.FirstName} ({message.From.Id}) отправил задачу:\n{message.Text}");
        }

        internal async Task NextPageTasks(CallbackQuery callbackQuery)
        {
            pageNum++;
            await bot.EditMessageTextAsync(
                chat.Id,
                callbackQuery.Message.MessageId,
                MessageBuilder.GetTasks(pageNum),
                replyMarkup: (InlineKeyboardMarkup)MessageBuilder.GetTasksMarkup()
            );
        }
        
        internal async Task BackPageTasks(CallbackQuery callbackQuery)
        {
            if (pageNum <= 1) return;
            pageNum--;
            await bot.EditMessageTextAsync(
                chat.Id,
                callbackQuery.Message.MessageId,
                MessageBuilder.GetTasks(pageNum),
                replyMarkup: (InlineKeyboardMarkup)MessageBuilder.GetTasksMarkup()
            );
        }
        
        internal async Task SendTasksList(CallbackQuery callbackQuery)
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
