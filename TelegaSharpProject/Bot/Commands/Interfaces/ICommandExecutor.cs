using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Interfaces;

public interface ICommandExecutor
{
    public Task Execute(Message message);
}