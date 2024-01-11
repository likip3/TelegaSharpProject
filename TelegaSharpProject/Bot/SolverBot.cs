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
    private ITelegramBotClient _botClient;
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

        _botClient = new TelegramBotClient(token);

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

        _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token); //Запускаем

        var me = await _botClient.GetMeAsync();
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

                    Console.WriteLine($"{textUser.FirstName} ({textUser.Id}) написал сообщение: {message.Text}");

                    switch (message.Text.ToLower())
                    {
                        case "/start":
                            var inlineButtons = new InlineKeyboardMarkup(
                                new List<InlineKeyboardButton[]>
                                {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Профиль", "b1"),
                                            InlineKeyboardButton.WithCallbackData("Таблица лидеров", "b2"),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Просмотр задач", "b3"),
                                            InlineKeyboardButton.WithCallbackData("Отправить задачу", "b4"),
                                        }
                                });
                            await botClient.SendTextMessageAsync(
                                message.Chat.Id,
                                "Что вы хотите?",
                                replyMarkup: inlineButtons
                            );

                            return;

                        default:
                            if (UserDB.GetUser(message.From.Id).TextMode == UserDB.TextModeEnum.WriteTask)
                            {
                                //todo ввод текста задачи
                            }
                            else if (UserDB.GetUser(message.From.Id).TextMode == UserDB.TextModeEnum.WriteSolve)
                            {
                                //todo ввод текста ответа
                            }
                            break;

                    }
                    break;
                case UpdateType.CallbackQuery:
                    {
                        var callbackQuery = update.CallbackQuery;

                        var CallbackUser = callbackQuery.From;

                        Console.WriteLine($"{CallbackUser.FirstName} ({CallbackUser.Id}) нажал на: {callbackQuery.Data}");
                        var chat = callbackQuery.Message.Chat;

                        switch (callbackQuery.Data)
                        {
                            case "b1":
                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    MessageBuilder.GetUserProfile(CallbackUser)
                                );
                                break;
                            case "b2":
                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    MessageBuilder.GetLeaderBoard()
                                );
                                break;
                            case "b3":
                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    MessageBuilder.GetTasks()
                                );
                                break;
                            case "b4":
                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    MessageBuilder.SendTaskITF()
                                );
                                break;

                        }
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