using TelegaSharpProject.Domain.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;

public interface IMessageBuilder
{
    public Task<string> GetUserProfile(CallbackQuery ctx);
    public Task<string> GetLeaderBoard();
    public Task<string> GetTask(int page);
}