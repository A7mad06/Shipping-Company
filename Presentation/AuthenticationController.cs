using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager serviceManager) : ControllerBase
    {

        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDTO>> Login(LoginDTO login)
        {
            var result = await serviceManager.AuthenticationServices.LoginAsync(login);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDTO>> Register(RegisterDTO register)
        {
            var result = await serviceManager.AuthenticationServices.RegisterAsync(register);
            return Ok(result);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult<UserResultDTO>> RegisterAdmim(RegisterDTO register)
        {
            var result = await serviceManager.AuthenticationServices.RegisterAdminAsync(register);
            return Ok(result);
        }
    }
}
