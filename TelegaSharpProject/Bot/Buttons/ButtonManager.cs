using TelegaSharpProject.Application.Bot.Buttons.Abstracts;
using TelegaSharpProject.Application.Bot.Buttons.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaSharpProject.Application.Bot.Buttons;

public class ButtonManager: IButtonManager
{
    private readonly Dictionary<string, Button> _buttonsDict = new();
    
    public ButtonManager(Button[] buttons)
    {
        foreach (var button in buttons)
            _buttonsDict.Add(button.SolverButton.Data, button);
    }

    public async Task Execute(CallbackQuery ctx)
    {
        if (ctx.Data != null && _buttonsDict.TryGetValue(ctx.Data.ToLower(), out var button))
            await button.ExecuteAsync(ctx);
    }

    public InlineKeyboardMarkup GetTitleButtons()
    {
        return new InlineKeyboardMarkup(
            new List<InlineKeyboardButton[]>
            {
                new InlineKeyboardButton[]
                {
                    _buttonsDict["profile"],
                    _buttonsDict["leaders"],
                },
                new InlineKeyboardButton[]
                {
                    _buttonsDict["viewtasks"],
                    _buttonsDict["mytasks"],
                    _buttonsDict["myanswers"],
                },
                new InlineKeyboardButton[]
                {
                    _buttonsDict["sendtask"],
                }
            });
    }

    public InlineKeyboardMarkup GetTaskMarkup(bool myTask = false)
    {
        if (!myTask)
            return new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>
                {
                    new InlineKeyboardButton[]
                    {
                        _buttonsDict["taskback"],
                        _buttonsDict["title"],
                        _buttonsDict["tasknext"],
                    },
                    new InlineKeyboardButton[]
                    {
                        _buttonsDict["answer"],
                    }
                }
            );
        
        
        return new InlineKeyboardMarkup(
            new List<InlineKeyboardButton[]>
            {
                new InlineKeyboardButton[]
                {
                    _buttonsDict["taskback"],
                    _buttonsDict["title"],
                    _buttonsDict["tasknext"],
                },
                new InlineKeyboardButton[]
                {
                    _buttonsDict["answers"]
                }
            }
        );
    }

    public InlineKeyboardMarkup GetAnswerMarkup(bool withAnswer = false)
    {
        if (!withAnswer)
            return new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>
                {
                    new InlineKeyboardButton[]
                    {
                        _buttonsDict["taskback"],
                        _buttonsDict["title"],
                        _buttonsDict["tasknext"],
                    }
                }
            );
        
        
        return new InlineKeyboardMarkup(
            new List<InlineKeyboardButton[]>
            {
                new InlineKeyboardButton[]
                {
                    _buttonsDict["taskback"],
                    _buttonsDict["title"],
                    _buttonsDict["tasknext"],
                },
                new InlineKeyboardButton[]
                {
                    _buttonsDict["confirm"],
                }
            }
        );
    }
}