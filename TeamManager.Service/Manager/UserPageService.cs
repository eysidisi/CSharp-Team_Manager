using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Database;

namespace TeamManager.Service.Managera
{
    public class UserPageService
    {
        private IDatabaseConnection connection;

        public UserPageService(IDatabaseConnection connection)
        {
            this.connection = connection;
        }
    }
}
