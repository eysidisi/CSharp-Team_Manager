using Dapper.Contrib.Extensions;

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
