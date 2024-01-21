using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Interfaces
{
    internal interface ITaskInfo
    {
        public long Id { get; }

        public User? Topicaster { get; set; }


        public DateTime TopicStart { get; set; }


        public DateTime MentorEnd { get; set; }


        public double Price { get; set; }

        public bool Done { get; set; }
    }
}
