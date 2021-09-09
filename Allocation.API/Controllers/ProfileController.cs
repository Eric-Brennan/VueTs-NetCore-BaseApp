using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Allocation.API.Types;
using Allocation.API.Types.Profile;
using Allocation_API.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Allocation_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        [HttpPost("authenticate-login")]
        public async Task<ApiResponse<ProfileAccount>> AuthenticateLogin(ProfileAccount request)
        {
            var response = await _profileRepository.AuthenticateLogin(request.Username, request.Password);

            if(response != null && response.Success)
            {
                return new ApiResponse<ProfileAccount>((int)HttpStatusCode.OK, "Success", 1, response);
            }

            return new ApiResponse<ProfileAccount>((int)HttpStatusCode.NotFound, "Fail", 0, response); ;
        }
    }
}
