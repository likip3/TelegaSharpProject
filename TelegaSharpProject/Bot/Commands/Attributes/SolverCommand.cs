namespace TelegaSharpProject.Application.Bot.Commands.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SolverCommand: Attribute
{
    public SolverCommand(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
}