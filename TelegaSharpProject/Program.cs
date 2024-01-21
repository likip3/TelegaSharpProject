using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Syntax;
using TelegaSharpProject.Application.Bot;
using TelegaSharpProject.Application.Bot.Buttons;
using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Interfaces;
using TelegaSharpProject.Application.Bot.Chats;
using TelegaSharpProject.Application.Bot.Chats.Interfaces;
using TelegaSharpProject.Application.Bot.Commands;
using TelegaSharpProject.Application.Bot.Commands.Abstracts;
using TelegaSharpProject.Application.Bot.Commands.Interfaces;
using TelegaSharpProject.Application.Bot.Settings;
using TelegaSharpProject.Application.Bot.Settings.Interfaces;
using TelegaSharpProject.Domain;
using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Data;
using Telegram.Bot;

namespace TelegaSharpProject.Application;

internal static class Program
{
    private static async Task Main()
    {
        var solverBot = ConfigureBot();
            
        // await solverBot.Start();

        await Task.Delay(-1);
    }

    private static SolverBot ConfigureBot()
    {
        var container = new StandardKernel();
            
        container
            .Bind(c => c
                .FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<Button>()
                .BindAllBaseClasses());

        container
            .Bind(c => c
                .FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<Command>()
                .BindAllBaseClasses());
            
        container
            .Bind<ISettingsManager>()
            .To<SettingsManager>()
            .InSingletonScope();

        container
            .Bind<IConnectionStringProvider>()
            .ToMethod(c => c
                .Kernel
                .Get<ISettingsManager>()
                .Load());

        container
            .Bind<ITokenProvider>()
            .ToMethod(c => c
                .Kernel
                .Get<ISettingsManager>()
                .Load());

        container
            .Bind<SolverBot>()
            .ToSelf()
            .InSingletonScope();
            
        container
            .Bind<ITelegramBotClient>()
            .ToMethod(c => c
                .Kernel
                .Get<SolverBot>()
                .BotClient);

        container
            .Bind<ICommandExecutor>()
            .To<CommandExecutor>()
            .InSingletonScope();

        container
            .Bind<IButtonManager>()
            .To<ButtonManager>()
            .InSingletonScope();

        container
            .Bind<IChatManager>()
            .To<ChatManager>()
            .InSingletonScope();

        container
            .Bind<TelegaSharpProjectContext>()
            .ToSelf()
            .InSingletonScope();

        container
            .Bind<IDBWorker>()
            .To<DBWorker>()
            .InSingletonScope();
        
        Test(container);
            
        return container.Get<SolverBot>();
    }
    
    private static async void Test(IResolutionRoot container)
    {
        var test = container.Get<IDBWorker>();

        var user = await test.GetUserInfoAsync(123123123L);
        
        Console.WriteLine($"{user.Id},{user.RegisteredAt},   {user.UserName}");
        
        await test.SendTaskAsync(123123123L, "Были жили 4 дедлайна...");
        
        var task = await test.GetUserTaskAsync(123123123L, 4);
        
        Console.WriteLine($"{task.Id},{task.Text},   {task.Topicaster.UserName}");
        
        await test.CommentTask(task.Id, 123123123L, "Ну ты и абобус");
        
        var comm = test.GetCommentsToTask(task.Id).Result[0];
        Console.WriteLine($"{comm.Id},{comm.Text},   {comm.ByUser.UserName}");
    }
}