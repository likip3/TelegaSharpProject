namespace TelegaSharpProject.Application.Bot.Settings;

public class AppSettings
{
    public string Token { get; }

    public AppSettings(string token)
    {
        Token = token;
    }
}