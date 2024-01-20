using TelegaSharpProject.Application.Bot.Buttons.Interfaces;
using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Chats;

public class SolverChat : ISolverChat
{
    private readonly IButtonManager _buttonManager;
    private readonly ITelegramBotClient _botClient;

    public SolverChat(Chat chat, IButtonManager buttonManager, ITelegramBotClient botClient)
    {
        _buttonManager = buttonManager;
        _botClient = botClient;
        ChatState = ChatState.WaitForCommand;
        Chat = chat;
        PageNum = 1;
    } 
    
    public ChatState ChatState { get; private set; }
    public Chat Chat { get; }
    public int PageNum { get; private set; }
    
    public async Task ToTitle()
    {
        var markUp = _buttonManager.GetTitleButtons();
        
        await _botClient.SendTextMessageAsync(
            Chat.Id,
            "Что вы хотите?",
            replyMarkup: markUp
        );

        ChatState = ChatState.WaitForCommand;
    }

    public bool TrySetPage(int page)
    {
        if(PageNum == page || page < 1) return false;
        
        PageNum = page;
        return true;
    }

    public void SetToInputState()
    {
        ChatState = ChatState.WaitForInput;
    }

    public void SetToCommandState()
    {
        ChatState = ChatState.WaitForCommand;
    }
}