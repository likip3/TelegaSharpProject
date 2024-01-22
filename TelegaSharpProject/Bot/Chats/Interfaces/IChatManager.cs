namespace TelegaSharpProject.Application.Bot.Chats.Interfaces;

public interface IChatManager
{
    public bool TryGetChat(long chatId, out ISolverChat chat);
    public ISolverChat GetChat(long chatId);
}