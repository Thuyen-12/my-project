namespace ALR.Data.Base
{
    public class DateFormat
    {
        public const string DateOnlyFormat = "yyyy-MM-dd";
        public const string DateTimeUTCFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
        public const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";
        public const string DateTimeISO8601 = "yyyy-MM-ddTHH:mm:ssZ";
        public const string DateUploadFormat = "dd/MM/yyyy";
        public const string DateShortTime = "M/d/yyyy h:mm:ss tt";
        public const string DateShortTimeSmoke = "M/d/yyyy H:mm:ss";
        public const string DefaultNotAvailableDate = "9999-01-01";
        public const string DateTimeOly = "yyyyMMdd";
    }

    public class BaseConstants
    {
        public const string COOKIE_KEY_USER = "_user_cookie";
        public const string USER_CLAIM_ID = "_user_id";
        public const string USER_CLAIM_EMAIL = "_user_email";
        public const string USER_CLAIM_ROLE = "_user_role";
    }

    public static class AuthenticateDefault
    {
        public const string CookieScheme = "CustomCookie";
        public const string BearerScheme = "Bearer";
        public const string AuthProvider = "AuthProvider";

    }
}
