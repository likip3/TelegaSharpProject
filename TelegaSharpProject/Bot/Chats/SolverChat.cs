using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Infos;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Chats;

public class SolverChat : ISolverChat
{
    public SolverChat(Chat chat)
    {
        ChatState = ChatState.WaitForCommand;
        Chat = chat;
        PageNum = 1;
        TaskChatInfo = new TaskChatInfo();
    } 
    
    public ChatState ChatState { get; private set; }
    public InputType InputType { get; private set; }
    public Chat Chat { get; }
    public int PageNum { get; private set; }
    public ITaskChatInfo TaskChatInfo { get; }

    public void Reset()
    {
        TaskChatInfo.Reset();
        ChatState = ChatState.WaitForCommand;
    }

    public bool TrySetPage(int page)
    {
        if(PageNum == page || page < 1) return false;
        
        PageNum = page;
        return true;
    }

    public void SetToInputState(InputType inputType)
    {
        ChatState = ChatState.WaitForInput;
        InputType = inputType;
    }

    public void SetToCommandState()
    {
        ChatState = ChatState.WaitForCommand;
    }
}