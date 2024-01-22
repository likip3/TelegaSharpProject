using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[SolverCommand("/leaders")]
public class ToLeadersCommand : Command
{
    public ToLeadersCommand(Lazy<IMessageService> messageServiceFactory) : base(messageServiceFactory) { }
    
    public override async Task Execute(Message message)
    {
        await MessageServiceFactory.Value.LeaderBoard(message.Chat);
    }
}