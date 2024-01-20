namespace TelegaSharpProject.Application.Bot.Commands.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SolverCommand: Attribute
{
    public SolverCommand(string name, bool needsToCheck = true)
    {
        Name = name;
        NeedsToCheck = needsToCheck;
    }
    
    public string Name { get; }
    public bool NeedsToCheck { get; }
}