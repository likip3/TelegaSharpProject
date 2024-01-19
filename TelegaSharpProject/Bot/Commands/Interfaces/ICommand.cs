using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Interfaces;

public interface ICommand
{
    public void Execute(Message message);
}