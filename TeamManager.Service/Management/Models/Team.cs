using Dapper.Contrib.Extensions;

namespace TeamManager.Service.Management.Models
{
    [Table("Teams")]
    public class Team : IEquatable<Team>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public Team()
        {

        }

        public bool Equals(Team? other)
        {
            if (other == null)
            {
                return false;
            }

            return other.ID == ID && other.Name == Name && other.CreationDate == CreationDate;
        }
    }
}
