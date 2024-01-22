using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;

public interface IMessageService
{
    public void ShowLoadingAsync(CallbackQuery ctx);
    public Task ToTitleAsync(Chat chat);
    public Task UserProfileAsync(Chat chat, User user);
    public Task LeaderBoard(Chat chat);
    public Task TaskFirstPage(User user, Chat chat);
    public Task TaskAnotherPage(User user, Chat chat, int delta);
    public Task CreateEntity(Message message);
    public Task SendTask(Chat chat);
}