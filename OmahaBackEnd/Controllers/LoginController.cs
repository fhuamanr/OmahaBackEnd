using Microsoft.AspNetCore.Mvc;
using Omaha.Infra.Common;
using Omaha.Negocio.Interfaces;

namespace OmahaBackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserLogin _userLogin;

        public LoginController(IUserLogin userLogin)
        {
            _userLogin = userLogin;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUser login)
        {
            var result = await _userLogin.LoginUser(login);
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> ActualizaContraseña(ChangePass changePass)
        {
            var result = await _userLogin.ActualizaContraseña(changePass);
            return Ok(result);

        }
    }
}
