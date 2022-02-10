using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Service.Models
{
    [Table("Managers")]
    public class Manager
    {
        public Manager(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public Manager()
        {

        }
        public string UserName { get;  set; }
        public string Password { get; private set; }
    }
}
