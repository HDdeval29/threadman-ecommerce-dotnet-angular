using StoreCoreApi.DAL.Interface;
using StoreCoreApi.DAL.Model.Orders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Repository
{
    public class Order: IOrder
    {
        private readonly IDbServices _dbServices;

        public Order(IDbServices dbServices)
        {

            _dbServices = dbServices;
        }
        public async Task<OrderCommonResponse> AddToCart(CartItemDto cartItem)
        {
            var response = new OrderCommonResponse();

            try
            {
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable();

                ht.Add("@UserId", cartItem.UserId);
                ht.Add("@ProductId", cartItem.ProductId);
                ht.Add("@Quantity", cartItem.Quantity);
                ht.Add("@Size", cartItem.Size);

                dt = await _dbServices.DBase<DataTable>("SpAddToCart", ht);

                if (dt.Rows.Count>0)
                {
                    response.Status = "success";
                    response.Result = "Add Product into cart successfully";
                }
                else
                {
                    response.Status = "Failed";
                    response.Result = "";
                }

            }
            catch (Exception ex)
            {
                response.Error = ex.Message.ToString();
                
            }
            return response;
        }

        public async Task<OrderCommonResponse> UpdateCartItems(CartItemDto cartItemDto)
        {
            var response = new OrderCommonResponse();
            try
            {
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable();

                ht.Add("@UserId", cartItemDto.UserId);
                ht.Add("@ProductId", cartItemDto.ProductId);
                ht.Add("@Quantity", cartItemDto.Quantity);
                ht.Add("@Size", cartItemDto.Size);

                dt = await _dbServices.DBase<DataTable>("SpUpdateCartItem", ht);

                if (dt.Rows.Count>0)
                {
                    response.Status = "success";
                    response.Result = "Update Cart Items successfully";
                }
                else
                {
                    response.Status = "Failed";
                    response.Result = "";
                }
            }
            catch (Exception ex)
            {
                response.Error = ex.Message.ToString();
            }
            return response;
        }

        public async Task<OrderCommonResponse> RemoveProductFromCart(int userId, int productId)
        {
            var response = new OrderCommonResponse();
            try
            {
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable();

                ht.Add("@UserId", userId);
                ht.Add("@ProductId", productId);
                ht.Add("@Size", "");

                dt = await _dbServices.DBase<DataTable>("SpRemoveFromCart", ht);

                if (dt.Rows.Count > 0)
                {
                    response.Status = "success";
                    response.Result = "Remove Items from Cart successfully";
                }
                else
                {
                    response.Status = "Failed";
                    response.Result = "";
                }

            }
            catch (Exception ex)
            {

                response.Error = ex.Message.ToString(); 
            }
            return response;
        }
        //public async Task<GetCartItemsRes> GetCartItemsByUserID (int userId)
        //{

        //    var response = new GetCartItemsRes();
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        Hashtable ht = new Hashtable();

        //        ht.Add("@UserId", userId);

        //        dt = await _dbServices.DBase<DataTable>("SpGetCartItems", ht);

        //        if (dt.Rows.Count > 0)
        //        {
        //            response.Name = Convert.ToString(dt.Rows[0]["Name"]);
        //            response.Price = Convert.ToDecimal(dt.Rows[0]["Price"]);
        //            response.cartItemId = Convert.ToInt32(dt.Rows[0]["CartItemId"]);
        //            response.Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
        //            response.Size = Convert.ToString(dt.Rows[0]["Size"]);
        //            response.ImageUrl = Convert.ToString(dt.Rows[0]["ImageUrl"]);
        //            response.Status = "success";
        //            //response.Result = "Remove Items from Cart successfully";
        //        }
        //        else
        //        {
        //            response.Status = "Failed";
        //            response.Result = "";
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        response.Error = ex.Message.ToString();
        //    }
        //    return response;
        //}


        public async Task<CartResponse> GetCartItemsByUserID(int userId)
        {
            var response = new CartResponse
            {
                CartItemsList = new List<GetCartItemsRes>(),
                Status = "success",
                Error = null
            };

            try
            {
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable
                {
                    { "@UserId", userId }
                };

                // Execute the stored procedure to get the cart items
                dt = await _dbServices.DBase<DataTable>("SpGetCartItems", ht);

                foreach (DataRow row in dt.Rows)
                {
                    var cartItem = new GetCartItemsRes
                    {
                        cartItemId = Convert.ToInt32(row["CartItemId"]),
                        ProductId = Convert.ToInt32(row["ProductId"]),
                        productName = Convert.ToString(row["Name"]),
                        productPrice = Convert.ToDecimal(row["Price"]),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        Size = Convert.ToString(row["Size"]),
                        productImageUrl = Convert.ToString(row["ImageUrl"])
                    };

                    // Handle image conversion to base64 if the file exists
                    string imagePath = cartItem.productImageUrl;
                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                        cartItem.productImageUrl = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                    }

                    // Add the populated cart item to the response data list
                    response.CartItemsList.Add(cartItem);
                }

                // Check if any items were found
                if (response.CartItemsList.Count == 0)
                {
                    response.Status = "Failed";
                    response.Error = "No items found in the cart.";
                }
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Error = ex.Message;
            }

            return response;
        }


        public async Task<string> UpdateCartCount(int userId)
        {
            // After updating, refresh the cart count

            string cartCount = string.Empty;
            Hashtable ht = new Hashtable();
            ht.Add("@UserId", userId);
            DataTable dt = await _dbServices.DBase<DataTable>("SpCartCount", ht);
            if (dt.Rows.Count > 0)
            {
                cartCount = Convert.ToString(dt.Rows[0]["CartCount"]);
            }
            return cartCount;
        }

        public async Task<OrderCommonResponse> CreateOrder(OrderRequest orderRequest)
        {
            var response = new OrderCommonResponse();
            try
            {
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable();

                ht.Add("@UserId", orderRequest.UserId);
                ht.Add("@TotalAmount", orderRequest.TotalAmount);
                ht.Add("@ShippingAddress", orderRequest.ShippingAddress);

                dt = await _dbServices.DBase<DataTable>("SpCreateOrder", ht);
                if (dt.Rows.Count > 0)
                {
                    string orderId = dt.Rows[0]["OrderId"].ToString();

                    response.Status = "Success";
                    response.orderId = orderId;
                }
                else
                {
                    response.Status = "Failed";
                }
            }
            catch (Exception ex)
            {
                response.Status = "Failed" + ex.Message.ToString();
            }
            return response;
        }
    }
}
