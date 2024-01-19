using TelegaSharpProject.Application.Bot.Interfaces;

namespace TelegaSharpProject.Application.Bot;

public class SolverConfig : IConfigLoader
{
    public string Token { get; private set; }
    public async void  LoadConfigAsync(string cfgFile)
    {
        Token = await GetToken(cfgFile);
    }

    private string _cfgFile;

    public SolverConfig()
    {
        //_cfgFile = cfgFile;
    }

    private static async Task<string?> GetToken(string cPath)
    {
        if (!System.IO.File.Exists(cPath))
        {
            Console.WriteLine("No Token");
            await Task.Delay(5000);
            Environment.Exit(1);
        }

        using var sr = new StreamReader(cPath);
        return await sr.ReadLineAsync(); ;
    }
}