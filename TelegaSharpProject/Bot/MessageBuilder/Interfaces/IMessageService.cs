using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;

public interface IMessageService
{
    public void ShowLoadingAsync(CallbackQuery ctx);
    public Task ToTitleAsync(Chat chat);
    public Task UserProfileAsync(Chat chat, User user);
    public Task LeaderBoard(Chat chat);
    public Task Task(User user, Chat chat);
    public Task CreateEntity(Message message);
    public Task SendTask(Chat chat);
}