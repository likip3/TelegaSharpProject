namespace TelegaSharpProject.Domain
{
    public class UserDB
    {
        public enum TextModeEnum
        {
            None,
            WriteTask,
            WriteSolve,
        }

        public TextModeEnum TextMode;
        public static UserDB GetUser(long id)
        {

            //todo

            return new();
        }
    }
}