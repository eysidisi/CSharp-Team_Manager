using Dapper.Contrib.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace TeamManager.Service.Management.Models
{
    [ExcludeFromCodeCoverage]
    [Table("Purposes")]
    public class Purpose : IEquatable<Purpose>
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

        public bool Equals(Purpose? other)
        {
            return ID == other.ID && UserName == other.UserName && PurposeText == other.PurposeText;
        }
    }
}
