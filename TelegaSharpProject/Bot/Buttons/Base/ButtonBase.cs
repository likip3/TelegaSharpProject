using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot.Buttons.Base
{
    public abstract class ButtonBase
    {
        protected readonly Lazy<ITelegramBotClient> BotClient;
        internal readonly string Data;
        private readonly string Text;
        
        internal abstract void Execute(CallbackQuery ctx);

        internal ButtonBase(Lazy<ITelegramBotClient> botClient)
        {
            BotClient = botClient;
            
            var attributes = GetType().GetCustomAttributes(typeof(SolverButton), true);
            if (attributes.Length > 0)
            {
                var solverButton = attributes[0] as SolverButton;
                Data = solverButton.Data.ToLower();
                Text = solverButton.Text;
            }
            else
            {
                throw new Exception("No Attribute");
            }
        }

        public static implicit operator InlineKeyboardButton(ButtonBase button)
        {
            return InlineKeyboardButton.WithCallbackData(button.Text, button.Data);
        }
    }
}
