using Dapper.Contrib.Extensions;

namespace TeamManager.Service.Wizard.Models
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
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
