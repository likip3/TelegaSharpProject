using System.Reflection;
using TelegaSharpProject.Application.Bot.Commands;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegaSharpProject.Application.Bot;

internal class SolverBot
{
    public static ITelegramBotClient botClient;
    private ReceiverOptions _receiverOptions;

    private Dictionary<string, MethodInfo> commandsDictionary = new();
    public async Task Start()
    {
#if DEBUG
        const string tPath = @"../../Token.txt";
#else
    const string tPath = @"Token.txt";
#endif


        if (!System.IO.File.Exists(tPath))
        {
            Console.WriteLine("No Token");
            await Task.Delay(5000);
            Environment.Exit(1);
        }

        string? token;
        using (var sr = new StreamReader(tPath))
            token = await sr.ReadLineAsync();

        botClient = new TelegramBotClient(token);

        _receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = new[] // https://core.telegram.org/bots/api#update
            {
                UpdateType.Message,
                UpdateType.CallbackQuery,
            },
            ThrowPendingUpdates = true,
        };
        using var cts = new CancellationTokenSource();

        LoadCommands();

        botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token); //Запускаем

        var me = await botClient.GetMeAsync();
        Console.WriteLine($"{me.FirstName} запущен!");
    }

    private void LoadCommands()
    {
        var classType = new CommandModule();
        var methods = classType.GetType().GetMethods().Where(m => m.GetCustomAttributes(typeof(Command), false).Length > 0)
            .ToArray();

        foreach (var method in methods)
        {
            commandsDictionary.Add(method.GetCustomAttribute<Command>().command.ToLower(), method);
            if(method.GetCustomAttribute<Command>().aliases.Length > 0)
                foreach (var alias in method.GetCustomAttribute<Command>().aliases)
                {
                    commandsDictionary.Add(alias.ToLower(), method);
                }
        }
    }

    private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    {
                        var message = update.Message;
                        var textUser = message.From;
                        Console.WriteLine($"{textUser.FirstName} ({textUser.Id}) написал: {message.Text}");

                        if (commandsDictionary.TryGetValue(message.Text.ToLower(), out var method))
                        {
                            CommandExecuter.ExecuteCommand(method,message, null);
                            //CommandExecuter.ExecuteCommand(method,message, new object[] { "messege" });
                        }
                        else
                        {
                            SolverChat.GetSolverChat(message).SendInput(message);
                        }
                        return;
                    }
                case UpdateType.CallbackQuery:
                    {
                        var callbackQuery = update.CallbackQuery;
                        var CallbackUser = callbackQuery.From;
                        Console.WriteLine($"{CallbackUser.FirstName} ({CallbackUser.Id}) нажал на: {callbackQuery.Data}");

                        SolverChat.GetSolverChat(callbackQuery).ButtonPressed(callbackQuery);
                        return;
                    }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    {
        var ErrorMessage = error switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}