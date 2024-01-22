using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;


[SolverCommand("/mytasks")]
public class ToMyTasksCommand : Command
{
    public ToMyTasksCommand(Lazy<IMessageService> messageServiceFactory) : base(messageServiceFactory) { }
    
    public override async Task Execute(Message message)
    {
        await MessageServiceFactory.Value.TaskFirstPageAsync(message.From, message.Chat);
    }
}