using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegaSharpProject.Application.Bot.Interfaces
{
    internal interface IConfigLoader
    {
        internal string Token { get; }

        internal void LoadConfigAsync(string cfgFile);
    }
}
