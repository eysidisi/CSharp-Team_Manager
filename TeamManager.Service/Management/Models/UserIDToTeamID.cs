using Dapper.Contrib.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace TeamManager.Service.Management.Models
{

    [ExcludeFromCodeCoverage]
    [Table("UserID_To_TeamID")]
    public class UserIDToTeamID : IEquatable<UserIDToTeamID>
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int TeamID { get; set; }
        public UserIDToTeamID()
        {

        }

        public bool Equals(UserIDToTeamID? other)
        {
            if (other == null)
            {
                return false;
            }

            return ID == other.ID &&
                   UserID == other.UserID &&
                   TeamID == other.TeamID;
        }
    }
}
