namespace TelegaSharpProject.Application.Bot.Settings;

public interface ISettingsManager
{
    public AppSettings Load();
}