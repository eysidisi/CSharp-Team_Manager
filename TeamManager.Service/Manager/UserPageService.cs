﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.Managera
{
    public class UserPageService
    {
        private IDatabaseConnection connection;

        public UserPageService(IDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public void AddUser(User user)
        {
            AddCreationTimeInfo(user);
            if (CheckIfUserIsValid(user))
            {
                connection.SaveUser(user);
            }
            else
            {
                throw new ArgumentException("You need to fill all fields!");
            }
        }

        public List<User> GetUsers()
        {
            return connection.GetUsers();
        }

        public bool DeleteUser(User user)
        {
            return connection.DeleteUser(user);
        }

        // TODO: Add check mechanisms
        private bool CheckIfUserIsValid(User user)
        {
            return true;
        }
        private void AddCreationTimeInfo(User user)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            user.CreationDate = sqlFormattedDate;
        }

        /// <summary>
        /// Used to transform data into DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }

            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
