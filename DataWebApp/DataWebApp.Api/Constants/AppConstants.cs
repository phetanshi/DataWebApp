namespace DataWebApp.Api.Constants
{
    public static class AppConstants
    {
        public const string AuthenticationType = "JwtServerAuth";
        public const int ExpiryTimeInMinutes = 60;
        public const string SUCCESS = "Success!";
        public const string FAIL = "Something went wrong!";
        public const string TOKEN_EXPIRED = "Authentication token expired!";
        public const string TOKEN_NOT_EXPIRED = "Authentication token is still alive!";

        public const string APP_POLICY = "AppPolicy";
        public const string ALLOW_SPECIFIC_ORG = "_myAllowSpecificOrigins";
    }
}
