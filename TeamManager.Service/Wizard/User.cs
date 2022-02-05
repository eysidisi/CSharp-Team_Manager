namespace TeamManager.Service.Wizard
{
    public class User
    {
        public string userName { get; private set; }
        public string password { get; private set; }

        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public User()
        {
        }
    }
}