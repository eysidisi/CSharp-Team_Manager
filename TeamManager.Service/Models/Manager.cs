using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Service.Models
{
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
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}
