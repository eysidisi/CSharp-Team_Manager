using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Service.Wizard
{
    public class Purpose
    {
        public Purpose(string userName, string purposeText)
        {
            UserName = userName;
            PurposeText = purposeText;
        }

        public string UserName { get; set; }
        public string PurposeText { get; set; }

    }
}
