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
            _solverBot = new SolverBot();
            await _solverBot.Start();

            await Task.Delay(-1);
        }
    }
}
