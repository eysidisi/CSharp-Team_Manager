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

            var dbPath = helperMethods.CreateTestDB_ReturnFilePath();

            // Create 100 random managers
            List<Manager> managers = CreateRandomManagers(1000);
            List<User> users = CreateRandomUsers(1000);
            List<Team> teams = CreateRandomTeams(100);
            List<UserIDToTeamID> userIDToTeamIDs = AddRandomMembers(users.Count, teams.Count);


            using (var conn = new SQLiteConnection($"Data Source={dbPath}"))
            {
                conn.Insert(managers);
                conn.Insert(users);
                conn.Insert(teams);
                conn.Insert(userIDToTeamIDs);
            }


            // Create 10 random teams


        }

        private static List<UserIDToTeamID> AddRandomMembers(int numOfUsers, int numOfTeams)
        {
            // %10 has no team, %10 member to all teams, %80 random

            int numberOfUsersThatMemberOfAllTeams = numOfUsers / 10;
            List<UserIDToTeamID> usersToTeamIDs = new List<UserIDToTeamID>();

            for (int userID = 1; userID <= numberOfUsersThatMemberOfAllTeams; userID++)
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

            for (int userID = numberOfUsersThatMemberOfAllTeams + 1;
                userID <= numOfUsers; userID++)
            {
                Random random = new Random();
                int numOfMemberTeams = random.Next(numOfTeams);

                for (int teamID = 1; teamID <= numOfMemberTeams; teamID++)
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
                string name = "Team_" + i;

                var randomDate = RandomDay();
                string creationDate = randomDate.ToString("yyyy-MM-dd HH:mm:ss");

                Team user = new Team()
                {
                    Name = name,
                    CreationDate = creationDate
                };

                teams.Add(user);
            }
            return teams;
        }

        private static List<User> CreateRandomUsers(int numOfUsers)
        {
            List<User> users = new List<User>();
            for (int i = 0; i < numOfUsers; i++)
            {
                string name = "UserName_" + i;
                string surname = "SurnameName_" + i;
                string title = "Title_" + i;
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
                string userName = "Manager_" + i;
                string password = "Password" + i;
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
