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
    public class CommentInfo : ICommentInfo
    {
        public CommentInfo(long id, long taskId, string? text, User? byUser, DateTime messageTime)
        {
            Id = id;
            TaskID = taskId;
            Text = text;
            ByUser = byUser;
            MessageTime = messageTime;
        }

        public CommentInfo(Comment c)
        {
            Id = c.Id;
            TaskID = c.TaskID;
            Text = c.Text;
            ByUser = c.ByUser;
            MessageTime = c.MessageTime;
        }

        public long Id { get; set; }

        public long TaskID { get; set; }

        public string? Text { get; set; }

        public User? ByUser { get; set; }

        public DateTime MessageTime { get; set; }

        public override string ToString()
        {
            //todo время в комменте
            return $"{ByUser.UserName}: {Text}";
        }
    }
}
