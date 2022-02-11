using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Service.Models
{
    [Table("Teams")]
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public Team()
        {

        }
    }
}
