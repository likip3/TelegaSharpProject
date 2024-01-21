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
        public Task<UserInfo> GetUserInfoAsync(long userId);

        public Task<UserInfo[]> GetLeaderBoardAsync();

        public Task<TaskInfo> GetTaskAsync(int page);

        public void CloseTask(long taskId);

        public void CommentTask(long taskId, long byUser, string text);

        public Task<CommentInfo[]> GetCommentsToTask(long taskId);

        public  Task<CommentInfo[]> GetCommentsFromUser(long userId);

        public void SendTaskAsync(long byUserId, string task);

        public Task RegisterUser(long userId, string userName);

        public Task<TaskInfo> GetUserTaskAsync(long userId, int page);
    }
}
