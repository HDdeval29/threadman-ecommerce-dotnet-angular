using StoreCoreApi.DAL.AdminPortal;
using StoreCoreApi.DAL.Interface;
using StoreCoreApi.DAL.Model.Masters;
using StoreCoreApi.DAL.Model.Orders;
using StoreCoreApi.DAL.Model.Product;
using StoreCoreApi.DAL.Model.UserCredentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.StoreDTO
{
    public class StoreDTO: IStoreDTO
    {
        private readonly IMasterServices _masterServices;
        private readonly IAdmin _admin;
        private readonly IUser _user;
        private readonly IProduct _product;
        private readonly IOrder _order;
        public StoreDTO(IMasterServices masterServices, IAdmin admin, IUser user, IProduct product, IOrder order)
        {
            _masterServices = masterServices;
            _admin = admin;
            _user = user;
            _product = product;
            _order = order;
        }
        public async Task<List<Category>> GetCategoryList()
        {
            return await _masterServices.GetCategoryList();
        }
        public async Task<List<Brands>> GetBrandsList()
        {
            return await _masterServices.GetBrandsList();
        }
        public async Task<List<Sizes>> GetSizesList()
        {
            return await _masterServices.GetSizesList();
        }
        public async Task<List<FitTypes>> GetFitTypesList()
        {
            return await _masterServices.GetFitTypesList();
        }
        public async Task<List<Colours>> GetColoursList()
        {
            return await _masterServices.GetColoursList();
        }
        public async Task<List<Gender>> GetGenderList()
        {
            return await _masterServices.GetGenderList();
        }


        public async Task<AdminCommonResponse> AdminLogin(AdminLogin request)
        {
             return await _admin.AdminLogin(request);
        }
        public async Task<AdminCommonResponse> InsertProduct(Productinsert Request)
        {
            return await _admin.InsertProduct(Request);
        }
        public async Task<ReturnResponse> InsertUserDetail(UserRegisterData userRegisterData)
        {
            return await _user.InsertUserDetail(userRegisterData);
        }
        public async Task<ReturnUserData> UserLogin(UserLoginData userLoginData)
        {
            return await _user.UserLogin(userLoginData);
        }

        public async Task<ReturnResponse> ForgotUserNameOrPassword(forgotPassword request)
        {
            return await _user.ForgotUserNameOrPassword(request);
        }



        public async Task<CommonResponse> AddProduct(insertProduct Request)
        {
            return await _product.AddProduct(Request);
        }
        public async Task<List<ProductModel>> GetALLProduct()
        {
            return await _product.GetALLProduct();
        }
        public async Task<ProductModel> GetProductById(int productId)
        {
            return await _product.GetProductById(productId);
        }
        public async Task<List<ProductModel>> SearchProductbyFilter(SearchProduct searchProduct)
        {
            return await _product.SearchProductbyFilter(searchProduct);
        }

        public async Task<List<ProductModel>> GetWishlistAsync(int userId)
        {
            return await _product.GetWishlistAsync(userId);
        }
        public async Task<CommonResponse> AddToWishlistAsync(WishlistReq wishlistReq)
        {
            return await _product.AddToWishlistAsync(wishlistReq);
        }
        public async Task<CommonResponse> RemoveFromWishlistAsync(WishlistReq wishlistReq)
        {
            return await _product.RemoveFromWishlistAsync(wishlistReq);
        }
        public async Task<List<Suggestion>> GetSuggestions(string search)
        {
            return await _product.GetSuggestions(search);
        }
        public async Task<List<ProductModel>> GetSearchBarProduct(string searchtext)
        {
            return await _product.GetSearchBarProduct(searchtext);
        }
        public async Task<OrderCommonResponse> AddToCart(CartItemDto cartItem)
        {
            return await _order.AddToCart(cartItem);
        }

        public async Task<OrderCommonResponse> UpdateCartItems(CartItemDto cartItemDto)
        {
            return await _order.UpdateCartItems(cartItemDto);
        }
        public async Task<OrderCommonResponse> RemoveProductFromCart(int userId, int productId)
        {
            return await _order.RemoveProductFromCart(userId, productId);
        }
        public async Task<CartResponse> GetCartItemsByUserID(int userId)
        {
            return await _order.GetCartItemsByUserID(userId);
        }

        public async Task<string> UpdateCartCount(int userId)
        {
            return await _order.UpdateCartCount(userId);
        }
        public async Task<OrderCommonResponse> CreateOrder(OrderRequest orderRequest)
        {
            return await _order.CreateOrder(orderRequest);
        }
    }
}
