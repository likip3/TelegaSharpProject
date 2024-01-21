using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegaSharpProject.Domain.Interfaces
{
    public interface IUserInfo
    {
        public long Id { get; set; }

        public string UserName { get; }

        public DateTime? RegisteredAt { get; }

        public int? Points { get; }
        
        public int? CompletedTasks { get; }
    }
}
