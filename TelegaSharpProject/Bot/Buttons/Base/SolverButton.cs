using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Requests;

namespace TelegaSharpProject.Application.Bot.Buttons.Base
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class SolverButton : Attribute
    {
        public string Data { get; }
        public string Text { get; }

        public SolverButton(string text, string data)
        {
            Data = data;
            Text = text;
        }

        public SolverButton(string text)
        {
            Data = text;
            Text = text;
        }

    }
}
