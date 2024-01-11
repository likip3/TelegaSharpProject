using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Domain.Bot;
using TelegaSharpProject

namespace TelegaSharpProject
{
    internal class Program
    {
        private static SolverBot _solverBot;
        private static PostgreSQL _postgre;
        private static async Task Main()
        {
            _postgre = new PostgreSQL();
            await _postgre.Connect();

            _solverBot = new SolverBot();
            await _solverBot.Start();

            await Task.Delay(-1);
        }
    }
}
