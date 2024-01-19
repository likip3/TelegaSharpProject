using TelegaSharpProject.Application.Bot.Commands.Attributes;
using TelegaSharpProject.Application.Bot.Commands.Interfaces;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Commands;


//todo 
[TelegaSharpProject.Application.Bot.Commands.Attributes.SolverCommand("/title")]
public class ToTitleCommand : ICommand
{
    private readonly Func<Message, SolverChat> _chatFactory;
    
    public ToTitleCommand(Func<Message, SolverChat> chatFactory)
    {
        _chatFactory = chatFactory;
    }
    
    public void Execute(Message message)
    {
        _chatFactory(message).ToTitle();
    }
}