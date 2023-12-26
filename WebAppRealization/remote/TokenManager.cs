namespace WebAppIImpl.remote
{
    public static class TokenManager
    {
        private static string _token;

        public static string? Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}