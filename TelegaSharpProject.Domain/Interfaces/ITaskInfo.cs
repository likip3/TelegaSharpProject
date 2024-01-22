using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Interfaces
{
    public interface ITaskInfo
    {
        public long Id { get; }

        public IUserInfo Creator { get;  }
        
        public DateTime TaskStart { get; }
        
        public DateTime? MentorEnd { get; }
        
        public IUserInfo? Mentor { get; }
        
        public string Text { get; }
        
        public double Price { get; }

        public bool Done { get; }
    }
}
