using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Model.Orders
{
    public class CartItemDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
    }
    public class GetCartItemsRes
    {
        public int cartItemId { get; set; }
        public int ProductId { get; set; }
        //public string Name { get; set; }
        public decimal productPrice { get; set; }
        public string productName { get; set; }
        public string productImageUrl { get; set; }

        //public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        //public string ImageUrl { get; set; }
    }

    public class CartResponse
    {
        public string Status { get; set; }    
        public List<GetCartItemsRes> CartItemsList { get; set; } 
        public object Error { get; set; }      
    }

    public class OrderCommonResponse
    {
        public string Error { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public string orderId { get; set; }
    }

    public class OrderRequest
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
    }

}
