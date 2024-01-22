using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[SolverCommand("/start")]
public class StartCommand : Command
{
    public StartCommand(Lazy<IMessageService> messageServiceFactory) : base(messageServiceFactory) { }
    
    public override async Task Execute(Message message)
    {
        await MessageServiceFactory.Value.UserProfileAsync(message.Chat, message.From);
    }
}