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
                (await GetUserAnswersAsync(user.Id, user))
                .Count(a => a.Closed));
        }
        
        public async Task<IUserInfo> GetUserInfoByIdAsync(long userId)
        {
            var user = await _db.Users.FindAsync(userId);

            return new UserInfo(
                user, 
                (await GetUserAnswersAsync(user.Id, user))
                .Count(a => a.Closed));
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

            return new TaskInfo(result.Entity, user);
        }

        private async Task<ITaskInfo> GetTaskInfoAsync(Work work)
        {
            var user = await _db.Users.FindAsync(work.CreatorId);
            
            if (!work.Done)
                return new TaskInfo(work, user);

            var answerInfo = await GetAnswerInfoAsync(await GetAnswerAsync(work.Answer));

            return new TaskInfo(work, user, answerInfo);
        }

        public async Task<ITaskInfo> GetTaskByIdAsync(long taskId)
        {
            var task = await _db.Works.FindAsync(taskId);
            var user = await _db.Users.FindAsync(task.CreatorId);

            return new TaskInfo(task, user);
        }

        public async Task<ITaskInfo[]> GetTasksAsync(long userId, bool fromThisUser = false)
        {
            var works = await _db.Works
                .OrderByDescending(w => w.TopicStart)
                .Where(t => (t.CreatorId != userId) == !fromThisUser)
                .Where(t => fromThisUser || !t.Done)
                .ToArrayAsync();
            
            return await Task.WhenAll(works.Select(GetTaskInfoAsync));
        }

        public async Task<ITaskInfo> CloseTaskAsync(long taskId, long answerId)
        {
            var task = await _db.Works.FindAsync(taskId);
            var answer = await GetAnswerAsync(answerId);
            var user = await _db.Users.FindAsync(answer.ByUserId);
            
            task.Close(answer);
            answer.Close();
            user.IncreasePoints(task.Price);
            
            _db.Works.Update(task);
            _db.Answers.Update(answer);
            _db.Users.Update(user);
            
            await _db.SaveChangesAsync();

            return new TaskInfo(
                task,
                await _db.Users.FindAsync(task.CreatorId),
                user,
                answer);
        }

        private async Task<Answer> GetAnswerAsync(long answerId)
        {
            return await _db.Answers.FindAsync(answerId);
        }

        public async Task<IAnswerInfo> CreateAnswerAsync(long taskId, long byUser, string text)
        {
            var user = await _db.Users.FindAsync(byUser);
            var comment = new Answer(taskId, user.Id, text);
            var answer = await _db.Answers.AddAsync(comment);
            await _db.SaveChangesAsync();

            return new AnswerInfo(answer.Entity, user);
        }

        public async Task<IAnswerInfo[]> GetTaskAnswersAsync(long taskId)
        {
            var answers = await _db.Answers
                .Where(answer => answer.TaskId == taskId)
                .ToArrayAsync();
            
            return await Task.WhenAll(answers.Select(a => GetAnswerInfoAsync(a)));
        }

        private async Task<IAnswerInfo> GetAnswerInfoAsync(Answer answer, User? user = null)
        {
            user ??= await _db.Users.FindAsync(answer.ByUserId);
            var task = await _db.Works.FindAsync(answer.TaskId);
            var creator = await _db.Users.FindAsync(task.CreatorId);

            var a1 = new AnswerInfo(answer, user);
            var t = new TaskInfo(task, creator, a1);

            return new AnswerInfo(answer, user, t);
        }
        
        public async Task<IAnswerInfo[]> GetUserAnswersAsync(long userId, User? user = null)
        {
            user ??= await _db.Users.FindAsync(userId);
            
            var answers = await _db.Answers
                .OrderByDescending(a => a.AnswerTime)
                .Where(a => a.ByUserId == userId)
                .ToArrayAsync();
            
            return await Task.WhenAll
            (answers
                .Select(a => GetAnswerInfoAsync(a, user)));
        }

        public async Task TryRegisterUserAsync(IUserInfo userInfo)
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
