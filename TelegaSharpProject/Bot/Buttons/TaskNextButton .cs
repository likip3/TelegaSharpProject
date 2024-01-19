using System;
using TelegaSharpProject.Application.Bot.Buttons.Base;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot.Buttons
{
    [SolverButton("Вперёд", "tasknext")]
    public class TaskNextButton : ButtonBase
    {
        public TaskNextButton(Lazy<ITelegramBotClient> botClient) : base(botClient) { }
        internal override async void Execute(CallbackQuery ctx)
        {
            await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
            var chat = SolverChat.GetSolverChat(ctx);

            chat.SetPage(chat.PageNum + 1);
            await BotClient.Value.EditMessageTextAsync(
                chat.chat,
                ctx.Message.MessageId,
                MessageBuilder.GetTasks(chat.PageNum),
                replyMarkup: (InlineKeyboardMarkup)MessageBuilder.GetTasksMarkup()
            );
        }
    }
}
