using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Command : Attribute
    {
        public string command;

        public string[] aliases = { };
        public Command(string command, string[] aliases = null)
        {
            this.command = command;
            if(aliases != null)
                this.aliases = aliases;
        }
    }
}
