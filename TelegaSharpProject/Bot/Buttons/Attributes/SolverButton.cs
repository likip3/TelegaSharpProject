namespace TelegaSharpProject.Application.Bot.Buttons.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SolverButton : Attribute
{
    public string Data { get; }
    public string Text { get; }

    public SolverButton(string text, string data)
    {
        Data = data.ToLower();
        Text = text;
    }

    public SolverButton(string text)
    {
        Data = text;
        Text = text;
    }

}