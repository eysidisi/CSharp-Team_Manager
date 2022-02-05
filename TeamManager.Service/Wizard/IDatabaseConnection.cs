namespace TeamManager.Service.Wizard
{
    public interface IDatabaseConnection
    {
        public bool CheckIfUserExists(User user);
        public void SavePurpose(string purpose, User user);
    }
}