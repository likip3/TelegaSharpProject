using System.Text;

namespace TelegaSharpProject.Application.Bot.Extensions;

public static class EnumerableExtensions
{
    public static string ToStringWithSeparator<T>(this IEnumerable<T> enumerable, string separator)
    {
        var sb = new StringBuilder();

        foreach (var e in enumerable)
        {
            sb.Append(e);
            sb.Append(separator);
        }
        
        if (sb.Length > 0)
            sb.Remove(sb.Length - 1, 1);

        return sb.ToString();
    }
}