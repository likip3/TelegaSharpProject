using TelegaSharpProject.Application.Bot.Buttons.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot.Buttons.Abstracts
{
    public abstract class Button
    {
        protected readonly Lazy<ITelegramBotClient> BotClient;
        internal readonly string Data;
        private readonly string Text;
        
        internal abstract Task Execute(CallbackQuery ctx);

        internal Button(Lazy<ITelegramBotClient> botClient)
        {
            BotClient = botClient;
            
            var attributes = GetType().GetCustomAttributes(typeof(SolverButton), true);
            if (attributes.Length > 0)
            {
                var solverButton = attributes[0] as SolverButton;
                
                //todo to field
                Data = solverButton.Data.ToLower();
                Text = solverButton.Text;
            }
            else
            {
                throw new Exception("No Attribute");
            }
        }

        public static implicit operator InlineKeyboardButton(Button button)
        {
            return InlineKeyboardButton.WithCallbackData(button.Text, button.Data);
        }
    }
}
