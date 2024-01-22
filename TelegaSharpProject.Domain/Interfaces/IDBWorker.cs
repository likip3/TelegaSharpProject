using TelegaSharpProject.Domain.Info;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Interfaces
{
    public interface IDbWorker : IDisposable
    {
        public Task<IUserInfo> GetUserInfoAsync(IUserInfo userInfo);

        public Task<IUserInfo> GetUserInfoByIdAsync(long userId);
        public Task<ITaskInfo> GetTaskByIdAsync(long taskId);

        public Task<IAnswerInfo[]> GetUserAnswersAsync(long userId, User? user = null);

        public Task<IUserInfo[]> GetLeaderBoardAsync();

        public Task<ITaskInfo[]> GetTasksAsync(long userId, bool fromThisUser = false);

        public Task CloseTaskAsync(long taskId, long answerId);

        public Task<IAnswerInfo> CreateAnswerAsync(long taskId, long byUser, string text);

        public Task<IAnswerInfo[]> GetTaskAnswersAsync(long taskId);

        public Task<ITaskInfo> CreateTaskAsync(long byUserId, string task);

        public Task TryRegisterUserAsync(IUserInfo userInfo);
    }
}
