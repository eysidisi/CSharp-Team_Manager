using TeamManager.Service.Models;

namespace TeamManager.Service.Database
{
    public interface IDatabaseConnection
    {
        public bool CheckIfManagerExists(Manager user);
        List<Purpose> GetPurposes(string userName);
        public void SavePurpose(Purpose purpose);
        Manager GetManager(string userName);
    }
}