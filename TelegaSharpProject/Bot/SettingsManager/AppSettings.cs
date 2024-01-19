namespace TelegaSharpProject.Application.Bot.SettingsManager;

public class AppSettings
{
    public string Token { get; }

    public AppSettings(string token)
    {
        Token = token;
    }
}