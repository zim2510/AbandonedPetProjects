namespace TalkOTC.Infrastructure
{
    public class AppException : Exception
    {
        public AppException(string message, int errorCode = 500) : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; set; }
    }
}
