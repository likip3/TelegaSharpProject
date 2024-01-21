using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons;

[SolverButton("Просмотр задач", "viewtasks")]
public class ViewTasksButton : Button
{
    private readonly Lazy<IChatManager> _chatManagerFactory;
    public ViewTasksButton(Lazy<ITelegramBotClient> botClient, Lazy<IChatManager> chatManagerFactory) : base(botClient)
    {
        _chatManagerFactory = chatManagerFactory;
    }
    
    internal override async Task Execute(CallbackQuery ctx)
    {
        if (!_chatManagerFactory.Value.TryGetChat(ctx.Message.Chat.Id, out var chat))
            return;
        
        chat.TrySetPage(1);
        
        await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
        await BotClient.Value.SendTextMessageAsync(
            chat.Chat,
            MessageBuilder.GetTasks(chat.PageNum),
            replyMarkup: MessageBuilder.GetTasksMarkup()
        );
    }
}