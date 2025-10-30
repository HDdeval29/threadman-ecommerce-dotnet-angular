using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCoreApi.BAL.Interface;
using StoreCoreApi.DAL.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreCoreApi.Controllers
{
    [Route("Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuthentication _jwtAuth;

        public AuthenticationController(IJwtAuthentication jwtAuth)
        {
            this._jwtAuth = jwtAuth;
        }

        [AllowAnonymous] // Allows access to this endpoint without authentication
        [Route("token")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = _jwtAuth.GenerateToken(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized(new { message = "Username or Password is incorrect" });
            return Ok(token);
        }

    }
}
