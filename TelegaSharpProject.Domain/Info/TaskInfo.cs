using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Info
{
    public class TaskInfo : ITaskInfo
    {
        public TaskInfo(long id, User? topicaster, DateTime topicStart, double price, string text)
        {
            Id = id;
            Topicaster = topicaster;
            TopicStart = topicStart;
            Price = price;
            Text = text;
        }

        public TaskInfo(Work work)
        {
            Id = work.Id;
            Topicaster = work.Topicaster;
            TopicStart = work.TopicStart;
            Price = work.Price;
            MentorEnd = work.MentorEnd;
            Done = work.Done;
            Text = work.Task;
        }

        public long Id { get; }

        public User? Topicaster { get; set; }


        public DateTime TopicStart { get; set; }


        public DateTime MentorEnd { get; set; }

        public string Text { get; set; }


        public double Price { get; set; }

        public bool Done { get; set; }
    }
}
