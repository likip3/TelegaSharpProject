using Microsoft.EntityFrameworkCore;
using TelegaSharpProject.Domain.Info;
using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Data;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain
{
    public class DBWorker : IDBWorker
    {
        private readonly TelegaSharpProjectContext _db;

        public DBWorker(TelegaSharpProjectContext db)
        {
            _db = db;
        }

        public async Task<UserInfo> GetUserInfoAsync(long userId)
        {
            var user = await _db.Users.FindAsync(userId);

            if (user is null)
                user = await CreateUserAsync(userId, userId.ToString().Remove(6, userId.ToString().Length - 1));

            return new UserInfo(user);
        }

        public async Task<UserInfo[]> GetLeaderBoardAsync()
        {
            return await _db.Users.OrderBy(u => u.Points).Take(10).Select(u => new UserInfo(u)).ToArrayAsync();
        }

        public async Task<TaskInfo> GetTaskAsync(int page)
        {
            var set = await _db.Works.OrderBy(w => w.TopicStart).ToArrayAsync();
            return new TaskInfo(set[page]);
        }

        public async Task<TaskInfo> GetUserTaskAsync(long userID, int page)
        {
            var set = await _db.Works.Where(t => t.Topicaster.Id == userID).OrderBy(w => w.TopicStart).ToArrayAsync();
            return new TaskInfo(set[page]);
        }

        public async Task CloseTask(long taskID)
        {
            var task = await _db.Works.FindAsync(taskID);
            task?.Close();
            _db.Works.Update(task);
            await _db.SaveChangesAsync();
        }

        public async Task CommentTask(long taskID, long byUser, string text)
        {
            var user = await _db.Users.FindAsync(byUser);
            var comment = new Comment(taskID, user, text);
            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();
        }

        public async Task<CommentInfo[]> GetCommentsToTask(long taskID)
        {
            return await _db.Comments.Where(c => c.TaskID == taskID).Select(c => new CommentInfo(c)).ToArrayAsync();
        }

        public async Task<CommentInfo[]> GetCommentsFromUser(long userID)
        {
            return await _db.Comments.Where(c => c.ByUser.Id == userID).Select(c => new CommentInfo(c)).ToArrayAsync();
        }

        public async Task SendTaskAsync(long byUserID, string task)
        {
            var user = await _db.Users.FindAsync(byUserID);
            var work = new Work(user, task);

            await _db.Works.AddAsync(work);
            await _db.SaveChangesAsync();
        }

        public async Task RegisterUser(long userId, string userName) => await CreateUserAsync(userId, userName);

        private async Task<User> CreateUserAsync(long userId, string userName)
        {
            var user = new User(userId, userName);
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
