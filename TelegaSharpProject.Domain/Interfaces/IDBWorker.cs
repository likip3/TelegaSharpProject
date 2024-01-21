using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegaSharpProject.Domain.Info;

namespace TelegaSharpProject.Domain.Interfaces
{
    public interface IDBWorker : IDisposable
    {
        public Task<IUserInfo> GetUserInfoAsync(IUserInfo userInfo);

        public Task<IUserInfo[]> GetLeaderBoardAsync();

        public Task<ITaskInfo> GetTaskAsync(int page);

        public Task CloseTask(long taskID);

        public Task CommentTask(long taskID, long byUser, string text);

        public Task<CommentInfo[]> GetCommentsToTask(long taskID);

        public  Task<CommentInfo[]> GetCommentsFromUser(long userID);

        public Task CreateTaskAsync(long byUserID, string task);

        public Task TryRegisterUser(IUserInfo userInfo);

        public Task<TaskInfo> GetUserTaskAsync(long userID, int page);
    }
}
