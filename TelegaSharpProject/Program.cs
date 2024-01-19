using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Extensions.Conventions;
using TelegaSharpProject.Application.Bot;
using TelegaSharpProject.Application.Bot.Buttons.Base;

namespace TelegaSharpProject
{
    internal class Program
    {
        private static async Task Main()
        {
            var solverBot = ConfigureBot();
            var token = await GetToken();
            
            await solverBot.Start(token);

            await Task.Delay(-1);
        }

        private static async Task<string> GetToken()
        {
#if DEBUG
            const string tPath = @"../../Token.txt";
#else
    const string tPath = @"Token.txt";
#endif


            if (!System.IO.File.Exists(tPath))
            {
                Console.WriteLine("No Token");
                await Task.Delay(5000);
                Environment.Exit(1);
            }
            
            using var sr = new StreamReader(tPath);
            
            return await sr.ReadLineAsync() 
                   ?? throw new FormatException();
        }

        private static SolverBot ConfigureBot()
        {
            var container = new StandardKernel();
            
            container
                .Bind(k => k
                    .FromThisAssembly()
                    .SelectAllClasses()
                    .InheritedFrom<ButtonBase>()
                    .BindAllBaseClasses());

            container
                .Bind<SolverBot>()
                .ToSelf()
                .InSingletonScope();

            return container.Get<SolverBot>();
        }
    }
}
