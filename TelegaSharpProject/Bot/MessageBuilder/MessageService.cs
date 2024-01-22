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
        ResetChat(chat);
        
        await BotClient.SendTextMessageAsync(
            chat.Id,
            "Что вы хотите?",
            replyMarkup: ButtonManager.GetTitleButtons()
        );
    }

    public async Task UserProfileAsync(Chat chat, User user)
    {
        ResetChat(chat);
        
        var userInfo = await Worker.GetUserInfoAsync(new UserInfo(user.Id, user.Username, chat.Id));
        
        await BotClient.SendTextMessageAsync(
            chat.Id,
            userInfo.ToMessage(),
            replyMarkup: ButtonManager.GetTitleButtons()
        );
    }

    public async Task LeaderBoard(Chat chat)
    {
        ResetChat(chat);
        
        var leaders = await Worker.GetLeaderBoardAsync();
        
        await BotClient.SendTextMessageAsync(
            chat.Id,
            leaders.ToMessage(),
            replyMarkup: ButtonManager.GetTitleButtons()
        );
    }

    private async Task<(ITaskInfo, InlineKeyboardMarkup)> GetTask(ISolverChat solverChat, User user)
    {
        var tasks = await Worker.GetTasksAsync(user.Id);
        var task = tasks[solverChat.TaskChatInfo.Page];
        var markup = ButtonManager.GetTaskMarkup();

        return (task, markup);
    }

    public async Task TaskFirstPage(User user, Chat chat)
    {
        var solverChat = ChatManager.GetChat(chat);
        solverChat.TaskChatInfo.Reset();

        string message;
        InlineKeyboardMarkup markup;

        try
        {
            (var taskInfo, markup) = await GetTask(solverChat, user);
            solverChat.TaskChatInfo.SetTask(taskInfo);
            
            message = taskInfo.ToMessage();
        }
        catch (IndexOutOfRangeException)
        {
            message = "На данный момент нет нерешенных задач";
            markup = ButtonManager.GetTitleButtons();
        }
        
        await BotClient.SendTextMessageAsync(
            chat.Id,
            message,
            replyMarkup: markup
        );
    }

    public async Task TaskAnotherPage(User user, Chat chat, int delta)
    {
        var solverChat = ChatManager.GetChat(chat);
        solverChat.TaskChatInfo.TrySetDeltaPage(delta);

        try
        {
            var (taskInfo, markup) = await GetTask(solverChat, user);
            solverChat.TaskChatInfo.SetTask(taskInfo);
            
            var message = taskInfo.ToMessage();
            
            await BotClient.SendTextMessageAsync(
                chat.Id,
                message,
                replyMarkup: markup
            );
        }
        catch (IndexOutOfRangeException)
        {
            await TaskFirstPage(user, chat);
        }
    }

    public async Task CreateEntity(Message message)
    {
        var solverChat = ChatManager.GetChat(message.Chat);

        string messageString;
        
        if (solverChat.InputType == InputType.TaskInput)
        {
            Console.WriteLine(message.From.Id);
            var taskInfo = await CreateTask(message.From, message.Text);
            messageString = $"Задача создана\n{taskInfo.ToMessage()}";
            solverChat.Reset();
        }
        else
        //todo
            return;
        
        await BotClient.SendTextMessageAsync(
            message.Chat.Id,
            messageString,
            replyMarkup: ButtonManager.GetTitleButtons()
        );
    }

    public async Task SendTask(Chat chat)
    {
        var solverChat = ChatManager.GetChat(chat);
        solverChat.SetToInputState(InputType.TaskInput);
        
        await BotClient.SendTextMessageAsync(
            chat.Id,
            "Введите текст задания"
        );
    }

    private async Task<ITaskInfo> CreateTask(User user, string text)
    {
        return await Worker.CreateTaskAsync(user.Id, text);
    }
    
    private ISolverChat ResetChat(Chat chat)
    {
        var solverChat = ChatManager.GetChat(chat);
        solverChat.Reset();

        return solverChat;
    }
}