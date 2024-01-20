namespace TelegaSharpProject.Application.Bot.Commands.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SolverCommand: Attribute
{
    public SolverCommand(string name, bool needsIndexing = true)
    {
        Name = name;
        NeedsIndexing = needsIndexing;
    }
    
    public string Name { get; }
    public bool NeedsIndexing { get; }
}