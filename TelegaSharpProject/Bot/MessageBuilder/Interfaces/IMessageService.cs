using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;

public interface IMessageService
{
    public void ShowLoadingAsync(CallbackQuery ctx);
    public Task ToTitleAsync(Chat chat);
    public Task UserProfileAsync(Chat chat, User user);
    public Task LeaderBoardAsync(Chat chat);
    public Task TaskFirstPageAsync(User user, Chat chat);
    public Task AnotherPageTaskAsync(User user, Chat chat, int delta);
    public Task CreateEntityAsync(Message message);
    public Task SendTaskAsync(Chat chat);
    public Task SendAnswerAsync(Chat chat);
    public Task AnswerFirstPageAsync(User user, Chat chat);
    public Task AnotherPageAnswerAsync(User user, Chat chat, int delta);
}