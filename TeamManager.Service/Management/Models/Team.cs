using Dapper.Contrib.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace TeamManager.Service.Management.Models
{
    [ExcludeFromCodeCoverage]
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
