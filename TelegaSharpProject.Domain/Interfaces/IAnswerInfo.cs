using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Interfaces
{
    public interface IAnswerInfo
    {
        public long Id { get; }

        public long TaskId { get; }

        public string Text { get; }
        
        public bool Closed { get; }

        public IUserInfo ByUser { get; }

        public DateTime MessageTime { get; }
    }
}
