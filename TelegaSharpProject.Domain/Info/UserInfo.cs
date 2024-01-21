using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Info
{
    public class UserInfo : IUserInfo
    {
        public UserInfo(long id, string userName, long chatId)
        {
            Id = id;
            UserName = userName;
            ChatId = chatId;
        }

        public UserInfo(User user)
        {
            ChatId = user.ChatId;
            Id = user.Id;
            RegisteredAt = user.RegisteredAt;
            UserName = user.UserName;
            Points = user.Points;
        }
        
        public UserInfo(User user, int completedTasks)
        {
            CompletedTasks = completedTasks;
            ChatId = user.ChatId;
            Id = user.Id;
            RegisteredAt = user.RegisteredAt;
            UserName = user.UserName;
            Points = user.Points;
        }

        public long Id { get; set; }
        public long ChatId { get; }

        public string UserName { get; }

        public DateTime? RegisteredAt { get; }

        public long? Points { get; }
        public int? CompletedTasks { get; }
    }
}
