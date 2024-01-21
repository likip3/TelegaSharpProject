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

        public async Task<TaskInfo> GetUserTaskAsync(long userId, int page)
        {
            var set = await _db.Works.Where(t => t.Topicaster.Id == userId).OrderBy(w => w.TopicStart).ToArrayAsync();
            return new TaskInfo(set[page]);
        }

        public async void CloseTask(long taskId)
        {
            var task = await _db.Works.FindAsync(taskId);
            task?.Close();
            _db.Works.Update(task);
            await _db.SaveChangesAsync();
        }

        public async void CommentTask(long taskId, long byUser, string text)
        {
            var user = await _db.Users.FindAsync(byUser);
            var comment = new Comment(taskId, user, text);
            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();
        }

        public async Task<CommentInfo[]> GetCommentsToTask(long taskId)
        {
            return await _db.Comments.Where(c => c.TaskID == taskId).Select(c => new CommentInfo(c)).ToArrayAsync();
        }

        public async Task<CommentInfo[]> GetCommentsFromUser(long userId)
        {
            return await _db.Comments.Where(c => c.ByUser.Id == userId).Select(c => new CommentInfo(c)).ToArrayAsync();
        }

        public async void SendTaskAsync(long byUserId, string task)
        {
            var user = await _db.Users.FindAsync(byUserId);
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
