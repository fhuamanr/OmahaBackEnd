using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Omaha.Infra.Common;
using Omaha.Negocio.Interfaces;

namespace OmahaBackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfile _profile;

        public ProfileController(IProfile profile)
        {
            _profile = profile;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfilePic(int IdUser)
        {
            var result = await _profile.GetProfilePic(IdUser);
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> InsertProfilePic(InsertProfilePic insertProfilePic)
        {
            var result = await _profile.InsertProfilePic(insertProfilePic);
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfile updateProfile)
        {
            var result = await _profile.UpdateProfile(updateProfile);
            return Ok(result);

        }


    }
}
