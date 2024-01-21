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
    
    public async Task<string> GetUserProfile(User user)
    {
        var userInfo = await _worker.GetUserInfoAsync(user.ToUserInfo());

        return
            $"Это ваш профиль, {userInfo.UserName}!\nОчки: {userInfo.Points}\nВыполнено задач: {userInfo.CompletedTasks}";
    }
}