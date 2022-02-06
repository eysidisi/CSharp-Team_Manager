namespace TeamManager.Service.Wizard
{
    public class User
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public User(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        public User()
        {
        }
    }
}