using TelegaSharpProject.Domain.Info;
using TelegaSharpProject.Domain.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Extensions;

public static class UserExtensions
{
    public static IUserInfo ToUserInfo(this User user)
    {
        return new UserInfo(user.Id, user.Username);
    }
}