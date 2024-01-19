using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Application.Bot;

namespace TelegaSharpProject
{
    internal class Program
    {
        private static SolverBot _solverBot;
        private static async Task Main()
        {
            var token = await GetToken();

            _solverBot = new SolverBot();
            await _solverBot.Start(token);

            await Task.Delay(-1);
        }

        private static async Task<string?> GetToken()
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

            string? token;
            using (var sr = new StreamReader(tPath))
                token = await sr.ReadLineAsync();
            return token;
        }
    }
}
