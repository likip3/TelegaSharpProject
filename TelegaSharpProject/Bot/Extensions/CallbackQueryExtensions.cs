using TelegaSharpProject.Domain.Info;
using TelegaSharpProject.Domain.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Extensions;

public static class CallbackQueryExtensions
{
    public static IUserInfo ToUserInfo(this CallbackQuery ctx)
    {
        return new UserInfo(ctx.From.Id, ctx.From.Username, ctx.Message.Chat.Id);
    }
}