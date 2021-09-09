using Allocation.API.Types.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Allocation_API.Repository.Interfaces
{
    public interface IProfileRepository
    {
        Task<ProfileAccount> AuthenticateLogin(string username, string password);
    }
}
