using Dapper.Contrib.Extensions;
using System;
using System.Data.SQLite;
using TeamManager.Service.Models;
using TeamManager.Service.Test.HelperMethods.SQLiteDB;
namespace TeamManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HelperMethods helperMethods = new HelperMethods();

            var smallDbPath = helperMethods.CreateTestDB_ReturnFilePath();
            var mediumDbPath = helperMethods.CreateTestDB_ReturnFilePath();
            var largeDbPath = helperMethods.CreateTestDB_ReturnFilePath();

            // Small DB
            CreateDB(smallDbPath, 100, 100, 1);

            // Medium DB
            CreateDB(mediumDbPath, 1000, 1000, 10);

            // Large DB
            CreateDB(largeDbPath, 10000, 10000, 100);
        }

        private static void CreateDB(string dbPath, int numOfManagers, int numOfUsers, int numberOfTeams)
        {
            List<Manager> managers = CreateRandomManagers(numOfManagers);
            List<User> users = CreateRandomUsers(numOfUsers);
            List<Team> teams = CreateRandomTeams(numberOfTeams);
            List<UserIDToTeamID> userIDToTeamIDs = AddRandomMembers(users.Count, teams.Count);

            Manager validManager = new Manager()
            {
                UserName = "validUserName",
                Password = "validPassword"
            };

            using (var conn = new SQLiteConnection($"Data Source={dbPath}"))
            {
                conn.Insert(validManager);
                conn.Insert(managers);
                conn.Insert(users);
                conn.Insert(teams);
                conn.Insert(userIDToTeamIDs);
            }
        }

        private static List<UserIDToTeamID> AddRandomMembers(int numOfUsers, int numOfTeams)
        {
            List<UserIDToTeamID> usersToTeamIDs = new List<UserIDToTeamID>();

            // %1 is a member of all of the teams
            int numberOfUsersMemberToAllTeams = Math.Max(numOfUsers / 100, 1);

            for (int userID = 1; userID <= numberOfUsersMemberToAllTeams; userID++)
            {
                for (int teamID = 1; teamID <= numOfTeams; teamID++)
                {
                    UserIDToTeamID userIDToTeamID = new UserIDToTeamID()
                    {
                        UserID = userID,
                        TeamID = teamID
                    };
                    usersToTeamIDs.Add(userIDToTeamID);
                }
            }


            for (int userID = numberOfUsersMemberToAllTeams + 1; userID <= numOfUsers; userID++)
            {
                Random random = new Random();
                int startingTeamIndex = random.Next(1, numOfTeams);
                int endingTeamIndex = Math.Min(random.Next(startingTeamIndex, startingTeamIndex + 2), numOfTeams);

                for (int teamID = startingTeamIndex; teamID <= endingTeamIndex; teamID++)
                {
                    UserIDToTeamID userIDToTeamID = new UserIDToTeamID()
                    {
                        UserID = userID,
                        TeamID = teamID
                    };
                    usersToTeamIDs.Add(userIDToTeamID);
                }
            }

            return usersToTeamIDs;
        }

        private static List<Team> CreateRandomTeams(int numOfTeams)
        {
            List<Team> teams = new List<Team>();
            for (int i = 0; i < numOfTeams; i++)
            {
                string name = "Team_" + (i + 1);

                var randomDate = RandomDay();
                string creationDate = randomDate.ToString("yyyy-MM-dd HH:mm:ss");

                Team team = new Team()
                {
                    Name = name,
                    CreationDate = creationDate
                };

                teams.Add(team);
            }
            return teams;
        }

        private static List<User> CreateRandomUsers(int numOfUsers)
        {
            List<User> users = new List<User>();
            for (int i = 0; i < numOfUsers; i++)
            {
                string name = "UserName_" + (i + 1);
                string surname = "SurnameName_" + (i + 1);
                string title = "Title_" + (i + 1);
                string phoneNumber = new string(i.ToString()[0], 10);

                var randomDate = RandomDay();
                string creationDate = randomDate.ToString("yyyy-MM-dd HH:mm:ss");

                User user = new User()
                {
                    Name = name,
                    PhoneNumber = phoneNumber,
                    Surname = surname,
                    Title = title,
                    CreationDate = creationDate,
                };

                users.Add(user);
            }
            return users;
        }

        static private Random gen = new Random();
        static DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        private static List<Manager> CreateRandomManagers(int numOfManagers)
        {
            List<Manager> managers = new List<Manager>();
            for (int i = 0; i < numOfManagers; i++)
            {
                string userName = "Manager_" + (i + 1);
                string password = "Password" + (i + 1);
                Manager manager = new Manager()
                {
                    UserName = userName,
                    Password = password
                };

                managers.Add(manager);
            }
            return managers;
        }
    }
}
