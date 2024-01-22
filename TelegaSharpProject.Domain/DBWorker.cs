using Microsoft.EntityFrameworkCore;
using TelegaSharpProject.Domain.Info;
using TelegaSharpProject.Domain.Interfaces;
using TelegaSharpProject.Infrastructure.Data;
using TelegaSharpProject.Infrastructure.Models;

namespace TelegaSharpProject.Domain
{
    public class DbWorker : IDbWorker
    {
        private readonly TelegaSharpProjectContext _db;

        public DbWorker(TelegaSharpProjectContext db)
        {
            _db = db;
        }

        public async Task<IUserInfo> GetUserInfoAsync(IUserInfo userInfo)
        {
            var user = await _db.Users.FindAsync(userInfo.Id) 
                       ?? await CreateUserAsync(userInfo);

            return new UserInfo(
                user, 
                (await GetUserTasksAsync(userInfo.Id)).Count(task => task.Done));
        }

        public async Task<IUserInfo[]> GetLeaderBoardAsync()
        {
            return await _db.Users
                .OrderBy(u => -u.Points)
                .Take(10)
                .Select(u => new UserInfo(u))
                .ToArrayAsync();
        }
        
        public async Task<ITaskInfo> CreateTaskAsync(long byUserId, string task)
        {
            var user = await _db.Users.FindAsync(byUserId);
            var work = new Work(user, task);

            var result = await _db.Works.AddAsync(work);
            await _db.SaveChangesAsync();

            return new TaskInfo(result.Entity);
        }

        public async Task<ITaskInfo[]> GetTasksAsync(long userId)
        {
            return await _db.Works
                // .OrderBy(w => w.TopicStart)
                .Where(task => task.TopicCreator.Id != userId)
                .Select(task => new TaskInfo(task))
                .ToArrayAsync();
        }

        private async Task<TaskInfo[]> GetUserTasksAsync(long userId)
        {
            return await _db.Works
                .OrderBy(w => w.TopicStart)
                .Where(t => t.TopicCreator.Id == userId)
                .Select(task => new TaskInfo(task))
                .ToArrayAsync();
        }
        
        public async Task<TaskInfo> GetUserTaskAsync(long userId, int page)
        {
            var set = await _db.Works
                .Where(t => t.TopicCreator.Id == userId)
                .OrderBy(w => w.TopicStart)
                .ToArrayAsync();
            
            return new TaskInfo(set[page]);
        }

        public async Task CloseTask(long taskId, long answerId)
        {
            var task = await _db.Works.FindAsync(taskId);
            task?.Close(await GetAnswerAsync(answerId));
            _db.Works.Update(task);
            await _db.SaveChangesAsync();
        }

        private async Task<Answer> GetAnswerAsync(long answerId)
        {
            return await _db.Answers.FindAsync(answerId);
        }

        public async Task CreateAnswerAsync(long taskId, long byUser, string text)
        {
            var user = await _db.Users.FindAsync(byUser);
            var comment = new Answer(taskId, user, text);
            await _db.Answers.AddAsync(comment);
            await _db.SaveChangesAsync();
        }

        public async Task<AnswerInfo[]> GetTaskAnswersAsync(long taskId)
        {
            return await _db.Answers
                .Where(c => c.TaskId == taskId)
                .Select(c => new AnswerInfo(c))
                .ToArrayAsync();
        }

        public async Task<AnswerInfo[]> GetCommentsFromUser(long userId)
        {
            return await _db.Answers.Where(c => c.ByUser.Id == userId).Select(c => new AnswerInfo(c)).ToArrayAsync();
        }

        public async Task TryRegisterUser(IUserInfo userInfo)
        {
            if (await _db.Users.FindAsync(userInfo.Id) is not null)
                return;
            
            await CreateUserAsync(userInfo);
        }

        private async Task<User> CreateUserAsync(IUserInfo userInfo)
        {
            var user = new User(userInfo.Id, userInfo.UserName, userInfo.ChatId);
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
