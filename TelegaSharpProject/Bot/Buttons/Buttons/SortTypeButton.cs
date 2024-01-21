using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Buttons.Buttons
{
    [SolverButton("Сортировка", "sorttype")]
    public class SortTypeButton : Button
    {
        public SortTypeButton(Lazy<ITelegramBotClient> botClient) : base(botClient) { }
        
        internal override async Task Execute(CallbackQuery ctx)
        {
            await BotClient.Value.AnswerCallbackQueryAsync(ctx.Id);
            //todo
        }
    }
}
