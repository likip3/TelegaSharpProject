using Microsoft.EntityFrameworkCore;
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
using TelegaSharpProject.Application.Bot.MessageBuilder;
using TelegaSharpProject.Application.Bot.MessageBuilder.Interfaces;
using TelegaSharpProject.Application.Bot.Settings;
using TelegaSharpProject.Application.Bot.Settings.Interfaces;
using TelegaSharpProject.Domain;
using TelegaSharpProject.Domain.Info;
using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Data;
using TelegaSharpProject.Infrastructure.Data.Interfaces;
using Telegram.Bot;

namespace TelegaSharpProject.Application;

internal static class Program
{
    private static async Task Main()
    {
        var container = ConfigureApplication();
        
        var solverBot = container.Get<SolverBot>();
        await solverBot.Start();

        await Task.Delay(-1);
    }

    private static StandardKernel ConfigureApplication()
    {
        var container = new StandardKernel();

        ConfigureSettings(container);
        ConfigureInfrastructure(container);
        ConfigureBot(container);
        
        // Test(container);
            
        return container;
    }

    private static void ConfigureBot(IBindingRoot container)
    {
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
            .Bind<IMessageService>()
            .To<MessageService>()
            .InSingletonScope();
    }

    private static void ConfigureInfrastructure(IBindingRoot container)
    {
        container
            .Bind<TelegaSharpProjectContext>()
            .ToSelf()
            .InSingletonScope();

        container
            .Bind<IDbWorker>()
            .To<DbWorker>()
            .InSingletonScope();

        container
            .Bind<DbContext>()
            .To<TelegaSharpProjectContext>()
            .InSingletonScope();
    }

    private static void ConfigureSettings(IBindingRoot container)
    {
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
    }
    
    private static async void Test(IResolutionRoot container)
    {
        var test = container.Get<IDbWorker>();

        // await test.TryRegisterUser(123123123L, "fdsajfdsaj");

        // var user = await test.GetUserInfoAsync(new UserInfo(123L, "fsadfsa"));
        //
        // Console.WriteLine($"{user.Id},{user.RegisteredAt},   {user.UserName}");
        //
        // await test.CreateTaskAsync(123123123L, "Были жили 4 дедлайна...");
        //
        // var task = await test.GetUserTaskAsync(123123123L, 0);
        //
        // Console.WriteLine($"{task.Id},{task.Text},   {task.TopicCreator.UserName}");
        //
        // await test.CreateAnswerAsync(task.Id, 123123123L, "Ну ты и абобус");
        //
        // var comm = test.GetTaskAnswersAsync(task.Id).Result[0];
        // Console.WriteLine($"{comm.Id},{comm.Text},   {comm.ByUser.UserName}");
    }
}