using TelegaSharpProject.Application.Bot.Chats.Enums;
using TelegaSharpProject.Domain.Interfaces;

namespace TelegaSharpProject.Application.Bot.Chats.Interfaces;

public interface IAnswerChatInfo
{
    public IAnswerInfo? LastAnswer { get; }
    public int Page { get; }
    public int TrySetDeltaPage(int delta);
    public void Reset();
    public void SetAnswer(IAnswerInfo answerInfo);
    public void SetFrom(From from);
    public From From { get; }
}