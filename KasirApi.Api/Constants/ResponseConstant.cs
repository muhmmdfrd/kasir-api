namespace KasirApi.Api.Constants
{
    public class ResponseConstant
    {
        #region Code
        // Success Code
        public const string SUCCESS_CODE = "0000";

        // Error Code
        public const string INTERNAL_SERVER_ERROR_CODE = "5000";
        public const string BAD_REQUEST_CODE = "4000";
        public const string UNAUTHORIZED_CODE = "4400";
        public const string RECORD_NOT_FOUND_CODE = "4512";
        public const string UNAUTHORIZED_TOOLS_CODE = "4401";
        public const string DATABASE_CONNECTION_CODE = "3000";
        public const string DATABASE_UNIQUE_CODE = "3013";
        public const string REQUIRED_HEADERS_CODE = "6811";
        public const string FAILED_CODE = "9999";
        #endregion

        #region Message
        // Success Message
        public const string SUCCESS_MESSAGE = "Success.";

        // Error Message
        public const string INTERNAL_SERVER_ERROR = "Internal server error.";
        public const string BAD_REQUEST_MESSAGE = "Bad request.";
        public const string UNAUTHORIZED_MESSAGE = "Unauthorized.";
        public const string DATABASE_CONNECTION = "Unable to connect db server.";
        public const string REQUIRED_HEADERS = "headers is required.";
        public const string FAILED_MESSAGE = "Something went wrong.";

        #endregion
    }
}
