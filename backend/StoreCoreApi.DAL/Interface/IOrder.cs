using StoreCoreApi.DAL.Model.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Interface
{
    public interface IOrder
    {
        Task<OrderCommonResponse> AddToCart(CartItemDto cartItem);
        Task<OrderCommonResponse> UpdateCartItems(CartItemDto cartItemDto);
        Task<OrderCommonResponse> RemoveProductFromCart(int userId, int productId);
        Task<CartResponse> GetCartItemsByUserID(int userId);
        Task<string> UpdateCartCount(int userId);
        Task<OrderCommonResponse> CreateOrder(OrderRequest orderRequest);
    }
}
