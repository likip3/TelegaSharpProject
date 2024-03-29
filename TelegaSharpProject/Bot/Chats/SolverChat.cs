using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Infos;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;

namespace TelegaSharpProject.Application.Bot.Chats;

public class SolverChat : ISolverChat
{
    public SolverChat(long chatId)
    {
        ChatState = ChatState.None;
        ChatId = chatId;
        PageNum = 1;
        TaskChatInfo = new TaskChatInfo();
        AnswerChatInfo = new AnswerChatInfo();
    } 
    
    public ChatState ChatState { get; private set; }
    public InputType InputType { get; private set; }
    public long ChatId { get; }
    public int PageNum { get; private set; }
    public ITaskChatInfo TaskChatInfo { get; }
    public IAnswerChatInfo AnswerChatInfo { get; }

    public void Reset()
    {
        TaskChatInfo.Reset();
        AnswerChatInfo.Reset();
        ChatState = ChatState.None;
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

    public void SetToTaskState()
    {
        ChatState = ChatState.TaskState;
    }

    public void SetToCommandState()
    {
        ChatState = ChatState.None;
    }
    
    public void SetToAnswerState()
    {
        ChatState = ChatState.AnswerState;
    }
}