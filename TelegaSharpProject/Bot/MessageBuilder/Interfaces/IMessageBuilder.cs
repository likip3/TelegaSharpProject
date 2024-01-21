using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;

public interface IMessageBuilder
{
    public Task<string> GetUserProfile(User user);
}