using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Attributes;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;

[SolverCommand("/tasks")]
public class ToTasksCommand : Command
{
    public ToTasksCommand(Lazy<IMessageService> messageServiceFactory) : base(messageServiceFactory) { }


    public override async Task Execute(Message message)
    {
        await MessageServiceFactory.Value.Task(message.From, message.Chat);
    }
}