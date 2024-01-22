using TelegaSharpProject.Application.Bot.Buttons.Interfaces;
using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.Extensions;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using TelegaSharpProject.Domain.Info;
using TelegaSharpProject.Domain.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;


namespace TelegaSharpProject.Application.Bot.MessageBuilder;

public class MessageService : IMessageService
{
    private IDbWorker Worker { get; }
    private IChatManager ChatManager { get; }
    private IButtonManager ButtonManager { get; }
    private ITelegramBotClient BotClient { get; }

    public MessageService(
        IDbWorker worker,
        ITelegramBotClient botClient,
        IChatManager chatManager,
        IButtonManager buttonManager)
    {
        Worker = worker;
        ChatManager = chatManager;
        ButtonManager = buttonManager;
        BotClient = botClient;
    }
    
    public async void ShowLoadingAsync(CallbackQuery ctx)
    {
        await BotClient.AnswerCallbackQueryAsync(
            ctx.Id, 
            "Загружаем данные...",
            cacheTime: 1);
    }

    public async Task ToTitleAsync(Chat chat)
    {
        ResetChat(chat.Id);
        
        await SendMessageAsync(chat.Id, "Что вы хотите?");
    }

    public async Task UserProfileAsync(Chat chat, User user)
    {
        ResetChat(chat.Id);
        
        var userInfo = await Worker.GetUserInfoAsync(new UserInfo(user.Id, user.Username, chat.Id));
        
        await SendMessageAsync(chat.Id, userInfo.ToMessage());
    }

    public async Task LeaderBoardAsync(Chat chat)
    {
        ResetChat(chat.Id);
        
        var leaders = await Worker.GetLeaderBoardAsync();
        
        await SendMessageAsync(chat.Id, leaders.ToMessage());
    }

    private async Task<(ITaskInfo, InlineKeyboardMarkup)> GetTaskAsync(
        ISolverChat solverChat,
        User user,
        bool fromThisUser = false)
    {
        var tasks = await Worker.GetTasksAsync(user.Id, fromThisUser);
        
        var task = tasks[solverChat.TaskChatInfo.Page];
        var markup = ButtonManager.GetTaskMarkup(fromThisUser);

        return (task, markup);
    }
    
    private async Task<(IAnswerInfo, InlineKeyboardMarkup)> GetAnswerAsync(
        ISolverChat solverChat,
        User user,
        bool fromThisUser = false)
    {
        var answers = fromThisUser 
            ? await Worker.GetUserAnswersAsync(user.Id)
            : await Worker.GetTaskAnswersAsync(solverChat.TaskChatInfo.LastTask.Id);
        
        var answer = answers[solverChat.AnswerChatInfo.Page];
        var markup = ButtonManager.GetAnswerMarkup(!fromThisUser && !answer.Closed);

        return (answer, markup);
    }

    public async Task TaskFirstPageAsync(User user, Chat chat)
    {
        var solverChat = ChatManager.GetChat(chat.Id);
        solverChat.TaskChatInfo.Reset();

        var fromThisUser = solverChat.TaskChatInfo.From == From.Me;

        string message;
        InlineKeyboardMarkup markup;

        try
        {
            (var taskInfo, markup) = await GetTaskAsync(solverChat, user, fromThisUser);
            solverChat.TaskChatInfo.SetTask(taskInfo);
            
            message = taskInfo.ToMessage();
        }
        catch (IndexOutOfRangeException)
        {
            message =
                solverChat.TaskChatInfo.From == From.Me 
                    ? "Вы еще не выложили ни одной задачи" 
                    : "На данный момент нет нерешенных задач";
            
            markup = ButtonManager.GetTitleButtons();
        }
        
        await SendMessageAsync(chat.Id, message, markup);
    }

    public async Task AnotherPageTaskAsync(User user, Chat chat, int delta)
    {
        var solverChat = ChatManager.GetChat(chat.Id);
        solverChat.TaskChatInfo.TrySetDeltaPage(delta);
        
        var fromThisUser = solverChat.TaskChatInfo.From == From.Me;

        try
        {
            var (taskInfo, markup) = await GetTaskAsync(solverChat, user, fromThisUser);
            solverChat.TaskChatInfo.SetTask(taskInfo);
            
            var message = taskInfo.ToMessage();

            await SendMessageAsync(chat.Id, message, markup);
        }
        catch (IndexOutOfRangeException)
        {
            await TaskFirstPageAsync(user, chat);
        }
    }

    public async Task CreateEntityAsync(Message message)
    {
        var solverChat = ChatManager.GetChat(message.Chat.Id);

        string messageString;
        
        if (solverChat.InputType == InputType.TaskInput)
        {
            var taskInfo = await CreateTaskAsync(message.From, message.Text);
            messageString = $"Задача создана\n{taskInfo.ToMessage()}";
            solverChat.Reset();
        }
        else if (solverChat.InputType == InputType.AnswerInput)
        {
            var answerInfo = await Worker.CreateAnswerAsync(solverChat.TaskChatInfo.LastTask.Id, message.From.Id, message.Text);
            messageString = $"Ответ отправлен\n{answerInfo.ToMessage()}";
            solverChat.Reset();

            var taskInfo = await Worker.GetTaskByIdAsync(answerInfo.TaskId);

            var notification = $"Пришел новый ответ на задание\n\n" +
                               $"Задание:\n{taskInfo.ToMessage()}\n\n" +
                               $"Ответ:\n{answerInfo.ToMessage()}";

            SendMessageAsync(taskInfo.Creator.ChatId, notification);
        }
        else
        {
            await ToTitleAsync(message.Chat);
            return;
        }
        
        await SendMessageAsync(message.Chat.Id, messageString);
    }

    public async Task SendTaskAsync(Chat chat)
    {
        var solverChat = ChatManager.GetChat(chat.Id);
        solverChat.SetToInputState(InputType.TaskInput);
        
        await SendMessageAsync(
            chat.Id, 
            "Введите текст задания", 
            new InlineKeyboardMarkup(Enumerable.Empty<InlineKeyboardButton>()));
    }

    public async Task SendAnswerAsync(Chat chat)
    {
        var solverChat = ChatManager.GetChat(chat.Id);
        solverChat.SetToInputState(InputType.AnswerInput);

        await SendMessageAsync(
            chat.Id, 
            "Введите текст ответа",
            new InlineKeyboardMarkup(Enumerable.Empty<InlineKeyboardButton>()));
    }

    public async Task AnswerFirstPageAsync(User user, Chat chat)
    {
        var solverChat = ChatManager.GetChat(chat.Id);
        solverChat.TaskChatInfo.Reset();

        var fromThisUser = solverChat.AnswerChatInfo.From == From.Me;

        string message;
        InlineKeyboardMarkup markup;

        try
        {
            (var answerInfo, markup) = await GetAnswerAsync(solverChat, user, fromThisUser);
            solverChat.AnswerChatInfo.SetAnswer(answerInfo);
            
            message = answerInfo.ToMessage();
        }
        catch (IndexOutOfRangeException)
        {
            message =
                solverChat.TaskChatInfo.From == From.Me 
                    ? "Вы еще не написали ни одного ответа" 
                    : "На данный момент нет ответов на эту задачу";
            
            markup = ButtonManager.GetTitleButtons();
        }
        
        await SendMessageAsync(chat.Id, message, markup);
    }

    public async Task AnotherPageAnswerAsync(User user, Chat chat, int delta)
    {
        var solverChat = ChatManager.GetChat(chat.Id);
        solverChat.AnswerChatInfo.TrySetDeltaPage(delta);
        
        var fromThisUser = solverChat.AnswerChatInfo.From == From.Me;

        try
        {
            var (answerInfo, markup) = await GetAnswerAsync(solverChat, user, fromThisUser);
            solverChat.AnswerChatInfo.SetAnswer(answerInfo);
            
            var message = answerInfo.ToMessage();

            await SendMessageAsync(chat.Id, message, markup);
        }
        catch (IndexOutOfRangeException)
        {
            await AnswerFirstPageAsync(user, chat);
        }
    }

    private async Task SendMessageAsync(long chatId, string message, IReplyMarkup? markup = null)
    {
        await BotClient.SendTextMessageAsync(
            chatId,
            message,
            replyMarkup: markup ?? ButtonManager.GetTitleButtons()
        );
    }

    private async Task<ITaskInfo> CreateTaskAsync(User user, string text)
    {
        return await Worker.CreateTaskAsync(user.Id, text);
    }
    
    private ISolverChat ResetChat(long chatId)
    {
        var solverChat = ChatManager.GetChat(chatId);
        solverChat.Reset();

        return solverChat;
    }
}