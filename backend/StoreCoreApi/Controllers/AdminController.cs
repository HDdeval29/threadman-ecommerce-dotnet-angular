using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCoreApi.BAL.Interface;
using StoreCoreApi.DAL.AdminPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreCoreApi.Controllers
{
    [Authorize]
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IStoreServices _storeServices;
        public AdminController(IStoreServices storeServices)
        {
            _storeServices = storeServices;
        }

        [Route("GetAdmin")]
        [HttpPost]
        public async Task<IActionResult> AdminLogin([FromBody] AdminLogin resquest)
        {
            var GetAdmin = await _storeServices.AdminLogin(resquest);
            return Ok(GetAdmin);
        }

        [Route("InsertProduct")]
        [HttpPost]
        public async Task<IActionResult> InsertProduct([FromBody] Productinsert resquest)
        {
            var GetAdmin = await _storeServices.InsertProduct(resquest);
            return Ok(GetAdmin);
        }
    }
}
