using Dapper.Contrib.Extensions;

namespace TeamManager.Service.Models
{
    [Table("Managers")]
    public class Manager
    {
        public Manager()
        {

        }

        public Manager(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
