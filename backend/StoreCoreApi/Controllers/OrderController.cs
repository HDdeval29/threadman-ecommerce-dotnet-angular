using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreCoreApi.BAL.Interface;
using StoreCoreApi.DAL.Model.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreCoreApi.Controllers
{
    [Authorize]
    [Route("Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IStoreServices _storeServices;
        public OrderController(IStoreServices storeServices)
        {
            _storeServices = storeServices;
        }

        [Route("AddToCart")]
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDto cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest("Invalid cart item.");
            }

            var response = await _storeServices.AddToCart(cartItem);
            return Ok(response);
        }

        [Route("UpdateCartItems")]
        [HttpPost]
        public async Task<IActionResult> UpdateCartItems([FromBody] CartItemDto cartItem)
        {
            var res = await _storeServices.UpdateCartItems(cartItem);
            return Ok(res);
        }

        [HttpDelete("RemoveFromCart/{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            var res = await _storeServices.RemoveProductFromCart(userId, productId);
            return Ok(res);
        }

        [HttpGet("GetCartItems/{userId}")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
            var cartItems = await _storeServices.GetCartItemsByUserID(userId);

            if (cartItems == null)
            {
                return NotFound("No cart items found for this user.");
            }

            return Ok(cartItems);
        }
        [Route("UpdateCartCount/{userId}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCartCount(int userId)
        {
            var response = await _storeServices.UpdateCartCount(userId);
            return Ok(response);
        }

        [Route("CreateOrder")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
        {
            var response = await _storeServices.CreateOrder(orderRequest);
            return Ok(response);
        }
    }
}
