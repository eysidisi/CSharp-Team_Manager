namespace TeamManager.Service.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string CreationDate { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Title { get; private set; }


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