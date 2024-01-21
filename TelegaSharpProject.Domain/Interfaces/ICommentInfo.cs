using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Interfaces
{
    public interface ICommentInfo
    {
        public long Id { get; set; }

        public long TaskID { get; set; }

        public string? Text { get; set; }

        public User? ByUser { get; set; }

        public DateTime MessageTime { get; set; }
    }
}
