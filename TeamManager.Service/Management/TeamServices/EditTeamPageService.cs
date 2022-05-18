﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management.TeamServices
{
    public class EditTeamPageService
    {
        IManagementDatabaseConnection connection;
        Team team;
        public EditTeamPageService(IManagementDatabaseConnection connection, Team teamToEdit)
        {
            this.connection = connection;
            team = teamToEdit;
        }

        public void AddUserToTheTeam(User user)
        {
            if (CheckIfUserExistsInDB(user) == false)
            {
                throw new ArgumentException("User doesn't exist in the DB!");
            }

            if (CheckIfTeamExistsInDB() == false)
            {
                throw new ArgumentException("Team doesn't exist in the DB!");
            }

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { UserID = user.ID, TeamID = team.ID };

            if (CheckIfUserIsInTheTeam(userIDToTeamID))
            {
                throw new ArgumentException("User is already in the team!");
            }

            connection.SaveUserIDToTeamID(userIDToTeamID);
        }

        public List<User> GetAllUsers()
        {
            return connection.GetAllUsers();
        }

        public List<User> GetUsersInTeam()
        {
            if (CheckIfTeamExistsInDB() == false)
            {
                throw new ArgumentException("Team doesn't exist in DB!");
            }

            var users = connection.GetAllUsers();
            var userIDToTeamIDs = connection.GetAllUserIDToTeamID();

            if (users.Count == 0 || userIDToTeamIDs.Count == 0)
            {
                return new List<User>();
            }

            var userIDsBelongedToTeam = userIDToTeamIDs.Where(u => u.TeamID == team.ID)
                                                        .Select(u => u.UserID).ToList();
            return users.Where(u => userIDsBelongedToTeam.Contains(u.ID)).ToList();
        }

        public void RemoveUserFromTheTeam(User userToRemove)
        {
            if (CheckIfUserExistsInDB(userToRemove) == false)
            {
                throw new ArgumentException("User doesn't exist in the DB!");
            }

            if (CheckIfTeamExistsInDB() == false)
            {
                throw new ArgumentException("Team doesn't exist in the DB!");
            }

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID()
            {
                UserID = userToRemove.ID,
                TeamID = team.ID
            };

            if (CheckIfUserIsInTheTeam(userIDToTeamID) == false)
            {
                throw new ArgumentException("User is not in the team!");
            }

            UserIDToTeamID correctUserIDToTeamID = GetCorrectEntryFromDB(userIDToTeamID);

            connection.DeleteUserIDToTeamID(correctUserIDToTeamID);
        }

        private UserIDToTeamID GetCorrectEntryFromDB(UserIDToTeamID userIDToTeamID)
        {
            var allUserIDsToTeamIDs = connection.GetAllUserIDToTeamID();

            UserIDToTeamID? copyItem = allUserIDsToTeamIDs.Find(u => u.UserID == userIDToTeamID.UserID && u.TeamID == userIDToTeamID.TeamID);

            return copyItem;
        }

        private bool CheckIfUserIsInTheTeam(UserIDToTeamID userIDToTeamID)
        {
            var allUserIDsToTeamIDs = connection.GetAllUserIDToTeamID();

            // No entry
            if (allUserIDsToTeamIDs.Count == 0)
            {
                return false;
            }

            UserIDToTeamID? copyItem = allUserIDsToTeamIDs.Find(u => u.UserID == userIDToTeamID.UserID && u.TeamID == userIDToTeamID.TeamID);

            if (copyItem != null)
            {
                return true;
            }

            return false;
        }

        private bool CheckIfUserExistsInDB(User user)
        {
            return connection.GetAllUsers().Find(u => u.ID == user.ID) != null ? true : false;
        }

        private bool CheckIfTeamExistsInDB()
        {
            return connection.GetAllTeams().Find(t => t.ID == team.ID) != null ? true : false;
        }
    }
}
