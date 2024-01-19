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
    public class SolverCommand : Attribute
    {
        public string Command { get; }

        public string[] Aliases = { };
        public SolverCommand(string command, string[] aliases = null)
        {
            this.Command = command;
            if(aliases != null)
                this.Aliases = aliases;
        }
    }
}
