using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Interfaces;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot.Buttons;

public class ButtonManager: IButtonManager
{
    private readonly IChatManager _chatManager;
    private readonly Dictionary<string, Button> _buttonsDict = new();
    
    public ButtonManager(Button[] buttons, IChatManager chatManager)
    {
        _chatManager = chatManager;
        foreach (var button in buttons)
            _buttonsDict.Add(button.Data, button);
    }

    public async Task Execute(CallbackQuery ctx)
    {
        if (!_chatManager.TryGetChat(ctx.Message.Chat.Id, out var chat))
            return;
        
        chat.SetToCommandState();
        
        if (ctx.Data != null && _buttonsDict.TryGetValue(ctx.Data.ToLower(), out var button))
            await button.Execute(ctx);
    }

    public InlineKeyboardMarkup GetTitleButtons()
    {
        return new InlineKeyboardMarkup(
            new List<InlineKeyboardButton[]>
            {
                new InlineKeyboardButton[]
                {
                    _buttonsDict["profile"],
                    _buttonsDict["leaders"]
                },
                new InlineKeyboardButton[]
                {
                    _buttonsDict["viewtasks"],
                    _buttonsDict["sendtask"]
                },
                new InlineKeyboardButton[]
                {
                    _buttonsDict["mytasks"],
                    _buttonsDict["aboba"]
                }
            });
    }
}