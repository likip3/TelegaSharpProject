using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Interfaces;

public interface ICommandExecutor
{
    public void Execute(Message message);
}