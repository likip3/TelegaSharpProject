namespace TelegaSharpProject.Infrastructure.Data;

public interface IConnectionStringProvider
{
    public string ConnectionString { get; }
}