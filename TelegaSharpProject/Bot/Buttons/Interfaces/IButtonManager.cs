using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot.Buttons.Interfaces;

public interface IButtonManager
{
    public Task Execute(CallbackQuery ctx);

    public InlineKeyboardMarkup GetTitleButtons();

    public InlineKeyboardMarkup GetTaskMarkup(bool myTasks = false);
}