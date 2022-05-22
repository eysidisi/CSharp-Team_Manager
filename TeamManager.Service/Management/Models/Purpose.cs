using Dapper.Contrib.Extensions;

namespace TeamManager.Service.Management.Models
{
    [Table("Purposes")]
    public class Purpose
    {
        public Purpose(string userName, string purposeText)
        {
            UserName = userName;
            PurposeText = purposeText;
        }
        public Purpose()
        {

        }
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PurposeText { get; set; }
    }
}
