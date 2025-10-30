using StoreCoreApi.BAL.Interface;
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

namespace StoreCoreApi.BAL.Repository
{
    public class StoreServices: IStoreServices
    {
        private readonly IStoreDTO _storeDTO;
        public StoreServices(IStoreDTO storeDTO)
        {
            _storeDTO = storeDTO;
        }
        public async Task<List<Category>> GetCategoryList()
        {
            return await _storeDTO.GetCategoryList();
        }
        public async Task<List<Brands>> GetBrandsList()
        {
            return await _storeDTO.GetBrandsList();
        }
        public async Task<List<Sizes>> GetSizesList()
        {
            return await _storeDTO.GetSizesList();
        }
        public async Task<List<FitTypes>> GetFitTypesList()
        {
            return await _storeDTO.GetFitTypesList();
        }
        public async Task<List<Colours>> GetColoursList()
        {
            return await _storeDTO.GetColoursList();
        }
        public async Task<List<Gender>> GetGenderList()
        {
            return await _storeDTO.GetGenderList();
        }



        public async Task<AdminCommonResponse> AdminLogin(AdminLogin request)
        {
            return await _storeDTO.AdminLogin(request);
        }
        public async Task<AdminCommonResponse> InsertProduct(Productinsert Request)
        {
            return await _storeDTO.InsertProduct(Request);
        }


        public async Task<ReturnResponse> InsertUserDetail(UserRegisterData userRegisterData)
        {
             return await _storeDTO.InsertUserDetail(userRegisterData); 
        }

        public async Task<ReturnUserData> UserLogin(UserLoginData userLoginData)
        {
            return await _storeDTO.UserLogin(userLoginData);
        }
        public async Task<ReturnResponse> ForgotUserNameOrPassword(forgotPassword request)
        {
            return await _storeDTO.ForgotUserNameOrPassword(request);
        }
        public async Task<CommonResponse> AddProduct(insertProduct Request)
        {
            return await _storeDTO.AddProduct(Request);
        }
        public async Task<List<ProductModel>> GetALLProduct()
        {
            return await _storeDTO.GetALLProduct();
        }
        public async Task<ProductModel> GetProductById(int productId)
        {
            return await _storeDTO.GetProductById(productId);
        }

        public async Task<List<ProductModel>> SearchProductbyFilter(SearchProduct searchProduct)
        {
            return await _storeDTO.SearchProductbyFilter(searchProduct);
        }

        public async Task<List<ProductModel>> GetWishlistAsync(int userId)
        {
            return await _storeDTO.GetWishlistAsync(userId);
        }
        public async Task<CommonResponse> AddToWishlistAsync(WishlistReq wishlistReq)
        {
            return await _storeDTO.AddToWishlistAsync(wishlistReq);
        }
        public async Task<CommonResponse> RemoveFromWishlistAsync(WishlistReq wishlistReq)
        {
            return await _storeDTO.RemoveFromWishlistAsync(wishlistReq);
        }
        public async Task<List<Suggestion>> GetSuggestions(string search)
        {
            return await _storeDTO.GetSuggestions(search);
        }
        public async Task<List<ProductModel>> GetSearchBarProduct(string searchtext)
        {
            return await _storeDTO.GetSearchBarProduct(searchtext);
        }

        public async Task<OrderCommonResponse> AddToCart(CartItemDto cartItem)
        {
            return await _storeDTO.AddToCart(cartItem);
        }
        public async Task<OrderCommonResponse> UpdateCartItems(CartItemDto cartItemDto)
        {
            return await _storeDTO.UpdateCartItems(cartItemDto);
        }
        public async Task<OrderCommonResponse> RemoveProductFromCart(int userId, int productId)
        {
            return await _storeDTO.RemoveProductFromCart(userId, productId);
        }
        public async Task<CartResponse> GetCartItemsByUserID(int userId)
        {
            return await _storeDTO.GetCartItemsByUserID(userId);
        }
        public async Task<string> UpdateCartCount(int userId)
        {
            return await _storeDTO.UpdateCartCount(userId);
        }
        public async Task<OrderCommonResponse> CreateOrder(OrderRequest orderRequest)
        {
            return await _storeDTO.CreateOrder(orderRequest);
        }
    }
}
