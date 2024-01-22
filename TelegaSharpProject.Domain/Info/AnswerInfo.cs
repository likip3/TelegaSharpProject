using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Info
{
    public class AnswerInfo : IAnswerInfo
    {
        public AnswerInfo(Answer c)
        {
            Id = c.Id;
            TaskId = c.TaskId;
            Text = c.Text;
            ByUser = c.ByUser;
            MessageTime = c.AnswerTime;
        }

        public long Id { get; }

        public long TaskId { get; }

        public string Text { get; }

        public User ByUser { get; }

        public DateTime MessageTime { get; }
    }
}
