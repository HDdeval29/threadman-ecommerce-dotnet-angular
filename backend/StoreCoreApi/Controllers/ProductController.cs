using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCoreApi.BAL.Interface;
using StoreCoreApi.DAL.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreCoreApi.Controllers
{
    [Authorize]
    [Route("Products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IStoreServices _storeServices;
        public ProductController(IStoreServices storeServices)
        {
            _storeServices = storeServices;
        }
   
        [Route("AddProduct")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] insertProduct Request)
        {
            var InsertProduct = await _storeServices.AddProduct(Request);
            return Ok(InsertProduct);
        }

        [Route("GetProduct")]
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var products = await _storeServices.GetALLProduct();
            return Ok(products);
        }

        [Route("GetProductById")]
        [HttpGet]
        public async Task<IActionResult> GetProductById([FromHeader(Name = "ProductId")][Required] int ProductId)
        {
            var products = await _storeServices.GetProductById(ProductId);
            return Ok(products);
        }

        [Route("SearchProductbyFilter")]
        [HttpPost]
        public async Task<IActionResult> SearchProductbyFilter([FromBody] SearchProduct Request)
        {
            var products = await _storeServices.SearchProductbyFilter(Request);
            return Ok(products);
        }

        [Route("GetSearchBarProduct")]
        [HttpPost]
        public async Task<IActionResult> GetSearchBarProduct([FromBody] SearchBarProduct request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.SearchText))
            {
                return BadRequest("Search text cannot be empty.");
            }

            var products = await _storeServices.GetSearchBarProduct(request.SearchText);

            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }

        [HttpPost]
        [Route("AddToWishlist")]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistReq wishlistReq)
        {
            if (wishlistReq == null || wishlistReq.UserId <= 0 || wishlistReq.ProductId <= 0)
            {
                return BadRequest("Invalid input parameters.");
            }

            CommonResponse response = await _storeServices.AddToWishlistAsync(wishlistReq);

            if (response.Status == "Success")
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response.Error);
            }
        }

        [HttpGet]
        [Route("GetWishlist/{userId}")]
        public async Task<IActionResult> GetWishlist([FromRoute] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            List<ProductModel> products = await _storeServices.GetWishlistAsync(userId);
            return Ok(products);
        }

        [HttpDelete]
        [Route("RemoveFromWishlist")]
        public async Task<IActionResult> RemoveFromWishlist([FromBody] WishlistReq wishlistReq)
        {
            if (wishlistReq.UserId <= 0 || wishlistReq.ProductId <= 0)
            {
                return BadRequest("Invalid input parameters.");
            }

            CommonResponse response = await _storeServices.RemoveFromWishlistAsync(wishlistReq);

            if (response.Status == "Success")
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response.Error);
            }
        }

        [HttpGet]
        [Route("GetSuggestionsForSearch")]
        public async Task<IActionResult> GetSuggestions(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var suggestions = await _storeServices.GetSuggestions(search);
            return Ok(suggestions);
        }

        //[HttpPost("filter")]
        //public async Task<ActionResult<IEnumerable<Product>>> GetFilteredProducts([FromBody] FilterOptions filters)
        //{
        //    var products = await _productRepository.GetFilteredProductsAsync(
        //        filters.SearchText,
        //        filters.Category,
        //        filters.Brand,
        //        filters.Size,
        //        filters.FitType,
        //        filters.Color
        //    );

        //    return Ok(products);
        //}

        //[HttpGet("filter-options")]
        //public async Task<ActionResult<FilterOptions>> GetFilterOptions()
        //{
        //    var filterOptions = await _productRepository.GetFilterOptionsAsync();
        //    return Ok(filterOptions);
        //}
    }
}
