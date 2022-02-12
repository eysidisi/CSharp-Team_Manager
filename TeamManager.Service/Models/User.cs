using Dapper.Contrib.Extensions;

namespace TeamManager.Service.Models
{
    [Table("Users")]
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CreationDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }

        public User()
        {
        }
    }
}