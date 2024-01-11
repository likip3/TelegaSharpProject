using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegaSharpProject;

internal class Program
{
    private static ITelegramBotClient _botClient;
    private static ReceiverOptions _receiverOptions;
    static async Task Main()
    {
        const string tPath = @"../../Token.txt";


        if (!System.IO.File.Exists(tPath))
        {
            Console.WriteLine("No Token");
            await Task.Delay(5000);
            Environment.Exit(1);
        }

        string? token;
        using (var sr = new StreamReader(tPath))
            token = await sr.ReadLineAsync();

        _botClient = new TelegramBotClient(token);

        _receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = new[] // https://core.telegram.org/bots/api#update
            {
                UpdateType.Message,
            },
            ThrowPendingUpdates = true,
        };
        using var cts = new CancellationTokenSource();

        _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token); //Запускаем

        var me = await _botClient.GetMeAsync();
        Console.WriteLine($"{me.FirstName} запущен!");

        await Task.Delay(-1);
    }

    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                {
                    var message = update.Message;

                    var user = message.From;

                    Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");

                    var chat = message.Chat;
                    await botClient.SendTextMessageAsync(
                        chat.Id,
                        message.Text,
                        replyToMessageId: message.MessageId
                    );

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