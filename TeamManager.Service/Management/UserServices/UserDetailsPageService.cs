﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management.UserServices
{
    public class UserDetailsPageService
    {
        private IManagementDatabaseConnection connection;

        public UserDetailsPageService(IManagementDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public List<Team> GetTeamsThatUserIn(User user)
        {
            var allUserIDToTeamIDs = connection.GetAllUserIDToTeamID();
            var allTeams = connection.GetAllTeams();
            
            var teamIDs = allUserIDToTeamIDs.Where(a => a.UserID == user.ID)?.
                Select(a => a.TeamID).ToList();

            return allTeams.Where(a => teamIDs.Contains(a.ID)).ToList();
        }
    }
}
