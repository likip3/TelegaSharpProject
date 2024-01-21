using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Info
{
    public class UserInfo : IUserInfo
    {
        public UserInfo(long id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public UserInfo(User user)
        {
            Id = user.Id;
            RegisteredAt = user.RegisteredAt;
            UserName = user.UserName;
            Points = user.Points;
        }
        
        public UserInfo(User user, int completedTasks)
        {
            CompletedTasks = completedTasks;
            Id = user.Id;
            RegisteredAt = user.RegisteredAt;
            UserName = user.UserName;
            Points = user.Points;
        }

        public long Id { get; set; }

        public string UserName { get; }

        public DateTime? RegisteredAt { get; }

        public int? Points { get; }
        public int? CompletedTasks { get; }
    }
}
