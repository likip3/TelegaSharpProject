using TelegaSharpProject.Domain.Info;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain.Interfaces
{
    public interface IDbWorker : IDisposable
    {
        public Task<IUserInfo> GetUserInfoAsync(IUserInfo userInfo);

        public Task<IUserInfo[]> GetLeaderBoardAsync();

        public Task<ITaskInfo[]> GetTasksAsync(long userId);

        public Task CloseTask(long taskId, long answerId);

        public Task CreateAnswerAsync(long taskId, long byUser, string text);

        public Task<AnswerInfo[]> GetTaskAnswersAsync(long taskId);

        public  Task<AnswerInfo[]> GetCommentsFromUser(long userId);

        public Task<ITaskInfo> CreateTaskAsync(long byUserId, string task);

        public Task TryRegisterUser(IUserInfo userInfo);

        public Task<TaskInfo> GetUserTaskAsync(long userId, int page);
    }
}
