using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Info
{
    public class AnswerInfo : IAnswerInfo
    {
        public AnswerInfo(Answer answer, User byUser)
        {
            Id = answer.Id;
            TaskId = answer.TaskId;
            Text = answer.Text;
            ByUser = new UserInfo(byUser);
            Closed = answer.Closed;
            MessageTime = answer.AnswerTime;
        }
        
        public AnswerInfo(Answer answer, User byUser, ITaskInfo taskInfo)
        : this(answer, byUser)
        {
            TaskInfo = taskInfo;
        }

        public long Id { get; }

        public long TaskId { get; }

        public string Text { get; }

        public IUserInfo ByUser { get; }
        
        public bool Closed { get; }

        public DateTime MessageTime { get; }
        public ITaskInfo? TaskInfo { get; }
    }
}
