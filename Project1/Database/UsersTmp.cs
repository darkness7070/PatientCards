using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Database
{
    public partial class Users
    {
        public string DateOfBirth 
        { 
            get
            {
                    return DateBirth.HasValue ? DateBirth.Value.ToString("D") : "";
            }
        }
        public string PassportSN
        {
            get 
            { 
                return Passports != null ? Passports.Series + Passports.Number : ""; 
            }
        }
    }
}
