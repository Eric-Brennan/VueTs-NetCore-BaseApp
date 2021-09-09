using Allocation.API.Repository.Interfaces;
using Allocation.API.Types.Profile;
using Allocation.API.Repository.Interfaces;
using Allocation_API.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Allocation_API.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        IDataAccess _dataAccess;

        public ProfileRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        async public Task<ProfileAccount> AuthenticateLogin(string username, string password)
        {
            try
            {
                //await _dataAccess.ExecuteStoredProcedureAsync("sec.AuthenticateLogin",
                //new SqlParameter("@Username", username),
                //new SqlParameter("@Password", password));

                //if (_dataAccess.Read())
                //{
                //    ProfileAccount response = new ProfileAccount
                //    {
                //        FirstName = _dataAccess.GetValue<string>("FirstName"),
                //        LastName = _dataAccess.GetValue<string>("LastName"),
                //        AccountType = _dataAccess.GetValue<string>("AccountType"),
                //        AccountTypeID = _dataAccess.GetValue<int>("AccountTypeID"),
                //        Success = true,
                //        AirportID = _dataAccess.GetValue<int>("AirportFK"),
                //        BaseAirport = _dataAccess.GetValue<string>("IataCode"),
                //        CompanyID = _dataAccess.GetValue<int>("ConfigID"),
                //        ColleagueID = _dataAccess.GetValue<int>("ColleagueID"),
                //    };
                //    return response;
                //}

                return new ProfileAccount
                {
                    FirstName = "Test",
                    LastName = "Testington",
                    AccountType = "Admin",
                    Success = true
                };

            }
            catch (Exception e)
            {
                // TODO
            }

            ProfileAccount fail = new ProfileAccount
            {
                FirstName = "",
                LastName = "",
                AccountType = "",
                Success = false
            };

            return fail;
        }
    }
}
