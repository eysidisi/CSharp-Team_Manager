using Dapper.Contrib.Extensions;
using System;

namespace TeamManager.Service.Models
{
    [Table("Users")]
    public class User : IEquatable<User>
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

        public bool Equals(User? otherUser)
        {
            if (otherUser == null)
            {
                return false;
            }

            return otherUser.ID == ID &&
                   otherUser.Name == Name &&
                   otherUser.Surname == Surname &&
                   otherUser.CreationDate == CreationDate &&
                   otherUser.PhoneNumber == PhoneNumber &&
                   otherUser.Title == Title;
        }
    }
}