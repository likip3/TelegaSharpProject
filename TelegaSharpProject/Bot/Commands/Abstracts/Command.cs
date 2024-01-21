using TelegaSharpProject.Application.Bot.Commands.Attributes;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands.Abstracts;

public abstract class Command
{
    protected Command()
    {
        var attributes = GetType().GetCustomAttributes(typeof(SolverCommand), true);
        if (attributes.Length > 0)
        {
            var solverCommand = attributes[0] as SolverCommand;
            SolverCommand = solverCommand;
        }
        else
        {
            throw new Exception("No Attribute SolverCommand");
        }
    }
    
    public SolverCommand SolverCommand { get; }
    
    public abstract void Execute(Message message);
}