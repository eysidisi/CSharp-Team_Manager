﻿using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection
{
    public class TeamPageService
    {
        IManagerDatabaseConnection connection;

        public TeamPageService(IManagerDatabaseConnection connection)
        {
            this.connection = connection;
        }
        public List<Team> GetAllTeams()
        {
            return connection.GetAllTeams();
        }

        public void DeleteTeam(Team team)
        {
           if( connection.DeleteTeam(team)==false)
            {
                throw new ArgumentException("Can't delete the team!");
            }
        }
    }
}
