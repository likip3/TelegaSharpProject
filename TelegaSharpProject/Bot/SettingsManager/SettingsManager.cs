namespace TelegaSharpProject.Application.Bot.SettingsManager;

public class SettingsManager : ISettingsManager
{
    public AppSettings Load()
    {
        var settings = new AppSettings(GetToken());

        return settings;
    }
    
    private static string GetToken()
    {
#if DEBUG
        const string tPath = @"../../Token.txt";
#else
    const string tPath = @"Token.txt";
#endif

        if (!File.Exists(tPath))
            throw new FileNotFoundException($"Нет файла по пути {tPath}");
            
        using var sr = new StreamReader(tPath);
            
        return sr.ReadLine() ?? throw new FormatException("Не удалось прочитать строку");
    }
}