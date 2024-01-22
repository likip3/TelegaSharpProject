using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[SolverCommand("input", false)]
public class InputCommand : Command
{
    public InputCommand(Lazy<IMessageService> messageServiceFactory) : base(messageServiceFactory) { }
    
    public override async Task Execute(Message message)
    {
        await MessageServiceFactory.Value.CreateEntity(message);
    }
}