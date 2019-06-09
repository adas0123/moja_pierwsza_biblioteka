namespace biblia
{
    class LoginResult
    {
        public bool IsLoggedIn;
        public string AccType;
        public string Username;
        public LoginResult(bool isLoggedIn, string accType, string username)
        { 
            IsLoggedIn = isLoggedIn;
            AccType = accType;
            Username = username;
        }
    }
}

