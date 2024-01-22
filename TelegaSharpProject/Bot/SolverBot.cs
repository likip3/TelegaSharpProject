using TelegaSharpProject.Application.Bot.Buttons.Interfaces;
using TelegaSharpProject.Application.Bot.Commands.Interfaces;
using TelegaSharpProject.Application.Bot.Settings.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegaSharpProject.Application.Bot;

public class SolverBot
{
    private readonly IButtonManager _buttonManager;
    private readonly ICommandExecutor _executor;
    private readonly ReceiverOptions _receiverOptions;
    
    public ITelegramBotClient BotClient { get; }

    public SolverBot(IButtonManager buttonManager, ICommandExecutor executor, ITokenProvider tokenProvider)
    {
        _buttonManager = buttonManager;
        _executor = executor;
        BotClient = new TelegramBotClient(tokenProvider.Token);

        _receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = new[]
            {
                UpdateType.Message,
                UpdateType.CallbackQuery,
            },
            ThrowPendingUpdates = true,
        };
    }

    public async Task Start()
    {
        using var cts = new CancellationTokenSource();
        BotClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token);

        var me = await BotClient.GetMeAsync(cancellationToken: cts.Token);
        Console.WriteLine($"{me.FirstName} запущен!");
    }

    private async Task UpdateHandler(
        ITelegramBotClient botClient, 
        Update update,
        CancellationToken cancellationToken)
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
                    
                    await _executor.Execute(message);
                    return;
                }
                case UpdateType.CallbackQuery:
                {
                    var callbackQuery = update.CallbackQuery;
                    var CallbackUser = callbackQuery.From;
                    Console.WriteLine($"{CallbackUser.FirstName} ({CallbackUser.Id}) нажал на: {callbackQuery.Data}");
                    
                    await _buttonManager.Execute(callbackQuery);
                    
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