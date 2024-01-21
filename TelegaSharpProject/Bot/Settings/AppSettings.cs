using TelegaSharpProject.Application.Bot.Settings.Interfaces;
using TelegaSharpProject.Infrastructure.Data;

namespace TelegaSharpProject.Application.Bot.Settings;

public class AppSettings : IConnectionStringProvider, ITokenProvider
{
    public string Token { get; }
    public string ConnectionString { get; }

    public AppSettings(string token)
    {
        Token = token;
        ConnectionString = "Host=ep-curly-dew-a2rdjhqn.eu-central-1.aws.neon.tech;Port=5432;Database=solverDB;Username=likip3;Password=A4ILZ3FtXopO";
    }
}