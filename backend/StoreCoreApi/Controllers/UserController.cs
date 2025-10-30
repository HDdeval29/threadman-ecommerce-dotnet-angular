using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCoreApi.BAL.Interface;
using StoreCoreApi.DAL.Model.UserCredentials;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreCoreApi.Controllers
{
    [Authorize]
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IStoreServices _storeServices;
        public UserController(IStoreServices storeServices)
        {
            _storeServices = storeServices;
        }

        [Route("RegisterUser")]
        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] UserRegisterData userRegisterData)
        {
            var RegisterUser = await _storeServices.InsertUserDetail(userRegisterData);
            return Ok(RegisterUser);
        }

        [Route("GetUser")]
        [HttpPost]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginData userLoginData)
        {
            var GetUser = await _storeServices.UserLogin(userLoginData);
            return Ok(GetUser);
        }

        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotUserNameOrPassword([FromBody] forgotPassword request)
        {
            var ForgotPass = await _storeServices.ForgotUserNameOrPassword(request);
            return Ok(ForgotPass);
        }
    }
}
