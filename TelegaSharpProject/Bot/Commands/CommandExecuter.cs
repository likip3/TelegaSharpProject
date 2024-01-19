using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegaSharpProject.Application.Bot.Commands
{
    public static class CommandExecuter
    {
        public static void ExecuteCommand(MethodInfo method, Message message , object?[]? parameters)
        {
            //Console.WriteLine(method.Name);
            //Console.WriteLine(method.GetCustomAttribute<SolverCommand>().Command);

            var pars = new object[] { message };
            if(parameters != null)
                pars = pars.Concat(parameters).ToArray();

            object o = Activator.CreateInstance(method.ReflectedType);
            method.Invoke(o, pars);
        }
    }
}
