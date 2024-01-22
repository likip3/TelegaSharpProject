using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Interfaces
{
    public interface IAnswerInfo
    {
        public long Id { get; }

        public long TaskId { get; }

        public string Text { get; }

        public User ByUser { get; }

        public DateTime MessageTime { get; }
    }
}
