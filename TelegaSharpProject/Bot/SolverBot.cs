using TelegaSharpProject.Domain;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot;

internal class SolverBot
{
    public static ITelegramBotClient botClient;
    private ReceiverOptions _receiverOptions;
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

        botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token); //Запускаем

        var me = await botClient.GetMeAsync();
        Console.WriteLine($"{me.FirstName} запущен!");
    }

    private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:

                    var message = update.Message;
                    var textUser = message.From;
                    Console.WriteLine($"{textUser.FirstName} ({textUser.Id}) написал: {message.Text}");

                    switch (message.Text.ToLower())
                    {
                        case "/start":
                        case "/title":
                        case "главная":
                            SolverChat.GetSolverChat(message).ToTitle();
                            return;

                        default:
                            SolverChat.GetSolverChat(message).SendInput(message);
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