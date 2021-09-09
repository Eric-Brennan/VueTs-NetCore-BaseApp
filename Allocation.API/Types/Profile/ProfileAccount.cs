using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allocation.API.Types.Profile
{
    public class ProfileAccount
    {
        public bool Success { get; set; }
        public int ColleagueID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccountTypeID { get; set; }
        public string AccountType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSetDate { get; set; }
        public int CompanyID { get; set; }
        public int AirportID { get; set; }
        public string BaseAirport { get; set; }
    }
}
