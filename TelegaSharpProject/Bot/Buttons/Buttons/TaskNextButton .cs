using TelegaSharpProject.Application.Bot.Buttons.Base;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Вперёд", "tasknext")]
public class TaskNextButton : ButtonBase
{
    private readonly Lazy<IChatManager> _chatManagerFactory;
    public TaskNextButton(Lazy<ITelegramBotClient> botClient, Lazy<IChatManager> chatManagerFactory) : base(botClient)
    {
        _chatManagerFactory = chatManagerFactory;
    }
    internal override async Task Execute(CallbackQuery ctx)
    {
        await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
        if (!_chatManagerFactory.Value.TryGetChat(ctx.Message.Chat.Id, out var chat))
            return;
        
        chat.TrySetPage(1);

        chat.TrySetPage(chat.PageNum + 1);
        
        await BotClient.Value.EditMessageTextAsync(
            chat.Chat,
            ctx.Message.MessageId,
            MessageBuilder.GetTasks(chat.PageNum),
            replyMarkup: (InlineKeyboardMarkup)MessageBuilder.GetTasksMarkup()
        );
    }
}