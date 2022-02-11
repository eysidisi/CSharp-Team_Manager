using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection
{
    public class TeamPageService
    {
        IDatabaseConnection connection;

        public TeamPageService(IDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public void AddTeam(Team team)
        {
            if (CheckIfTeamExists(team))
            {
                throw new ArgumentException("A team with same name already exists!");
            }

            connection.SaveTeam(team);
        }

        private bool CheckIfTeamExists(Team team)
        {
            Team t = connection.GetTeamWithName(team.Name);

            if (t == null)
                return false;

            return true;
        }

        public void AddUserToTheTeam(Team team, User user)
        {
            if (CheckIfUserIsInTheTeam(user, team))
            {
                throw new ArgumentException("User is already in the same team!");
            }

            connection.SaveUserToTheTeam(user.ID, team.ID);
        }

        public List<Team> GetAllTeams()
        {
           return connection.GetAllTeams();
        }

        private bool CheckIfUserIsInTheTeam(User user, Team team)
        {
            return false;
        }

        public void DeleteUserFromTheTeam(Team team, User user)
        {
            if (CheckIfUserIsInTheTeam(user, team) == false)
            {
                throw new ArgumentException("User is not in the given team!");
            }

            connection.DeleteUserFromTheTeam(user.ID, team.ID);
        }

        public void DeleteTeam(Team team)
        {
            connection.DeleteTeam(team);
        }
    }
}
