using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegaSharpProject.Domain.Interfaces
{
    internal interface IUserInfo
    {
        public long Id { get; set; }

        public string? UserName { get; set; }

        public DateTime RegisteredAt { get; set; }

        public int Points { get; set; }
    }
}
