using System.Globalization;
using TelegaSharpProject.Domain.Interfaces;

namespace TelegaSharpProject.Application.Bot.Extensions;

public static class DomainEntitiesExtensions
{
    public static string ToMessage(this IUserInfo userInfo)
    {
        return
            $"Это ваш профиль, {userInfo.UserName}!\n" +
            $"Очки: {userInfo.Points}\n" +
            $"Выполнено задач: {userInfo.CompletedTasks}";
    }

    public static string ToMessage(this IUserInfo[] userInfos)
    {
        return userInfos
            .Select((user, i) => $"{i + 1}. {user.UserName}, очки: {user.Points}")
            .ToStringWithSeparator("\n");
    }

    public static string ToMessage(this ITaskInfo task)
    {
        var result = $"Создатель: {task.TopicCreator.UserName}\n" +
                     $"Количество очков за задачу: {task.Price}\n" +
                     $"Описание: \n{task.Text}\n";
        
        if (!task.Done)
            return result + "Статус: Открыта";

        return result +
               $"Статус: Закрыта, {task.MentorEnd.Value.ToString(CultureInfo.InvariantCulture)}" +
               $"Ментор: {task.Mentor.UserName}";
    }
}