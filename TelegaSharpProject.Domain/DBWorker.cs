using Microsoft.EntityFrameworkCore;
using TelegaSharpProject.Domain.Info;
using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Data;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain
{
    public class DBWorker : IDBWorker
    {
        private TelegaSharpProjectContext db;

        public DBWorker()
        {
            Connect();
        }

        private void Connect()
        {
            db = new TelegaSharpProjectContext();
        }

        public async Task<UserInfo> GetUserInfoAsync(long userId)
        {
            var user = await db.Users.FindAsync(userId);

            if (user is null)
                user = await CreateUserAsync(userId, userId.ToString().Remove(6, userId.ToString().Length - 1));

            return new UserInfo(user);
        }

        public async Task<UserInfo[]> GetLeaderBoardAsync()
        {
            return await db.Users.OrderBy(u => u.Points).Take(10).Select(u => new UserInfo(u)).ToArrayAsync();
        }

        public async Task<TaskInfo> GetTaskAsync(int page)
        {
            var set = await db.Works.OrderBy(w => w.TopicStart).ToArrayAsync();
            return new TaskInfo(set[page]);
        }

        public async Task<TaskInfo> GetUserTaskAsync(long userID, int page)
        {
            var set = await db.Works.Where(t => t.Topicaster.Id == userID).OrderBy(w => w.TopicStart).ToArrayAsync();
            return new TaskInfo(set[page]);
        }

        public async Task CloseTask(long taskID)
        {
            var task = await db.Works.FindAsync(taskID);
            task?.Close();
            db.Works.Update(task);
            await db.SaveChangesAsync();
        }

        public async Task CommentTask(long taskID, long byUser, string text)
        {
            var user = await db.Users.FindAsync(byUser);
            var comment = new Comment(taskID, user, text);
            await db.Comments.AddAsync(comment);
            await db.SaveChangesAsync();
        }

        public async Task<CommentInfo[]> GetCommentsToTask(long taskID)
        {
            return await db.Comments.Where(c => c.TaskID == taskID).Select(c => new CommentInfo(c)).ToArrayAsync();
        }

        public async Task<CommentInfo[]> GetCommentsFromUser(long userID)
        {
            return await db.Comments.Where(c => c.ByUser.Id == userID).Select(c => new CommentInfo(c)).ToArrayAsync();
        }

        public async Task SendTaskAsync(long byUserID, string task)
        {
            var user = await db.Users.FindAsync(byUserID);
            var work = new Work(user, task);

            await db.Works.AddAsync(work);
            await db.SaveChangesAsync();
        }

        public async Task RegisterUser(long userId, string userName) => await CreateUserAsync(userId, userName);

        private async Task<User> CreateUserAsync(long userId, string userName)
        {
            var user = new User(userId, userName);
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
