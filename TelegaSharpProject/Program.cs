using Ninject;
using Ninject.Extensions.Conventions;
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
using Telegram.Bot;

namespace TelegaSharpProject.Application;

internal static class Program
{
    private static async Task Main()
    {
        var solverBot = ConfigureBot();
            
        await solverBot.Start();

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
            .Bind<AppSettings>()
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
            
        return container.Get<SolverBot>();
    }
}