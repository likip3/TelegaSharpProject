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
        var result = $"Создатель: {task.Creator.UserName}\n" +
                     $"Количество очков за задачу: {task.Price}\n" +
                     $"Текст задачи: \n{task.Text}\n";
        
        if (!task.Done)
            return result + "Статус: Открыта";

        return result +
               $"Статус: Закрыта, {task.MentorEnd.Value.ToString(CultureInfo.InvariantCulture)}" +
               $"Ментор: {task.Mentor.UserName}";
    }

    public static string ToMessage(this IAnswerInfo answer, bool withTask = false)
    {
        var result = $"Ответ от: {answer.ByUser.UserName}\n" +
                     $"Время: {answer.MessageTime}\n" +
                     $"Ответ: \n{answer.Text}\n" +
                     "Статус: " + (answer.Closed ? "Закрыт" : "Открыт");

        if (!withTask)
            return result;

        return $"Задача:\n{answer.TaskInfo.ToMessage()}\n\n" +
               $"Ответ:\n" + result;
    }
}