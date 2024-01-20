using TelegaSharpProject.Application.Bot.Commands.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;


[Attributes.SolverCommand("/mytasks")]
public class ToMyTasksCommand : ICommand
{
    private readonly Lazy<ITelegramBotClient> _botClientfactory;
    
    public ToMyTasksCommand(Lazy<ITelegramBotClient> botClientfactory)
    {
        _botClientfactory = botClientfactory;
    }
    
    public async void Execute(Message message)
    {
        await _botClientfactory.Value.SendTextMessageAsync(
            message.Chat.Id,
            MessageBuilder.GetMyTasks(message.From));
    }
}