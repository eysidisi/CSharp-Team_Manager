namespace TeamManager.Service
{
    public interface IDatabaseConnection
    {
        public bool CheckIfUserExists(User user);
    }
}