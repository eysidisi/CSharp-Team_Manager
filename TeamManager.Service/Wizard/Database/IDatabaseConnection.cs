namespace TeamManager.Service.Wizard.Database
{
    public interface IDatabaseConnection
    {
        public bool CheckIfUserExists(User user);
        List<Purpose> GetPurposes(string userName);
        public void SavePurpose(Purpose purpose);
        User GetUser(string userName);
    }
}