using TelegaSharpProject.Application.Bot.Extensions;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using TelegaSharpProject.Domain.Interfaces;
using Telegram.Bot.Types;


namespace TelegaSharpProject.Application.Bot.MessageBuilder;

public class MessageBuilder : IMessageBuilder
{
    private readonly IDBWorker _worker;
    
    public MessageBuilder(IDBWorker worker)
    {
        _worker = worker;
    }
    
    public async Task<string> GetUserProfile(CallbackQuery ctx)
    {
        var userInfo = await _worker.GetUserInfoAsync(ctx.ToUserInfo());

        return
            $"Это ваш профиль, {userInfo.UserName}!\nОчки: {userInfo.Points}\nВыполнено задач: {userInfo.CompletedTasks}";
    }

    public async Task<string> GetLeaderBoard()
    {
        return (await _worker.GetLeaderBoardAsync())
            .Select((user, i) => $"{i + 1}. {user.UserName}, очки: {user.Points}")
            .ToStringWithSeparator("\n");
    }

    public async Task<string> GetTask(int page)
    {
        var task = await _worker.GetTaskAsync(page);
        
        // if (task.)

        return $"Создатель: {task.TopicCreator.UserName}\n";
    }
}