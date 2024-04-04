namespace KasirApi.Api.Exceptions
{
    public class UnauthorizedToolsException : Exception
    {
        public UnauthorizedToolsException()
        {
        }

        public UnauthorizedToolsException(string? message) : base(message)
        {
        }

        public UnauthorizedToolsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
