using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Domain.Interfaces;

namespace TelegaSharpProject.Application.Bot.Chats.Infos;

public class AnswerChatInfo : IAnswerChatInfo
{
    public IAnswerInfo? LastTask { get; private set; }
    public int Page { get; private set; }
    public From From { get; private set; }
    
    public int TrySetDeltaPage(int delta)
    {
        if (Page + delta < 0)
            return Page;

        Page += delta;
        return Page;
    }

    public void Reset()
    {
        Page = 0;
    }

    public void SetAnswer(IAnswerInfo answerInfo)
    {
        LastTask = answerInfo;
    }

    public void SetFrom(From from)
    {
        From = from;
    }
}