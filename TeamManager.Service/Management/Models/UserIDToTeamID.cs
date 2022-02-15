using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Service.Models
{
    [Table("UserID_To_TeamID")]
    public class UserIDToTeamID
    {
        public int ID { get; set; }
        public int  UserID{ get; set; }
        public int  TeamID{ get; set; }
        public UserIDToTeamID()
        {

        }
    }
}
