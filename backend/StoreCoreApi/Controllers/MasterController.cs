using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCoreApi.BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreCoreApi.Controllers
{
    [Authorize]
    [Route("Master")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IStoreServices _storeServices;

        public MasterController(IStoreServices storeServices)
        {
            _storeServices = storeServices;
        }

        [Route("GetCategoryList")]
        [HttpGet]
        public async Task<IActionResult> GetCategoryList()
        {
            var response = await _storeServices.GetCategoryList();
            return Ok(response);
        }
        [Route("GetBrandsList")]
        [HttpGet]
        public async Task<IActionResult> GetBrandsList()
        {
            var response = await _storeServices.GetBrandsList();
            return Ok(response);
        }
        [Route("GetSizesList")]
        [HttpGet]
        public async Task<IActionResult> GetSizesList()
        {
            var response = await _storeServices.GetSizesList();
            return Ok(response);
        }
        [Route("GetFitTypesList")]
        [HttpGet]
        public async Task<IActionResult> GetFitTypesList()
        {
            var response = await _storeServices.GetFitTypesList();
            return Ok(response);
        }
        [Route("GetColoursList")]
        [HttpGet]
        public async Task<IActionResult> GetColoursList()
        {
            var response = await _storeServices.GetColoursList();
            return Ok(response);
        }
        [Route("GetGenderList")]
        [HttpGet]
        public async Task<IActionResult> GetGenderList()
        {
            var response = await _storeServices.GetGenderList();
            return Ok(response);
        }
    }
}
