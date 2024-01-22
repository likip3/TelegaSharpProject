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
}