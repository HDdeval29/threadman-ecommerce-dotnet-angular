using StoreCoreApi.DAL.AdminPortal;
using StoreCoreApi.DAL.Model.Masters;
using StoreCoreApi.DAL.Model.Orders;
using StoreCoreApi.DAL.Model.Product;
using StoreCoreApi.DAL.Model.UserCredentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.BAL.Interface
{
    public interface IStoreServices
    {
        Task<List<Category>> GetCategoryList();
        Task<List<Brands>> GetBrandsList();
        Task<List<Sizes>> GetSizesList();
        Task<List<FitTypes>> GetFitTypesList();
        Task<List<Colours>> GetColoursList();
        Task<List<Gender>> GetGenderList();


        Task<AdminCommonResponse> AdminLogin(AdminLogin request);
        Task<AdminCommonResponse> InsertProduct(Productinsert Request);


        Task<ReturnResponse> InsertUserDetail(UserRegisterData userRegisterData);
        Task<ReturnUserData> UserLogin(UserLoginData userLoginData);
        Task<ReturnResponse> ForgotUserNameOrPassword(forgotPassword request);


        Task<CommonResponse> AddProduct(insertProduct Request);
        Task<List<ProductModel>> GetALLProduct();
        Task<ProductModel> GetProductById(int productId);
        Task<List<ProductModel>> SearchProductbyFilter(SearchProduct searchProduct);

        Task<List<ProductModel>> GetWishlistAsync(int userId);
        Task<CommonResponse> AddToWishlistAsync(WishlistReq wishlistReq);
        Task<CommonResponse> RemoveFromWishlistAsync(WishlistReq wishlistReq);
        Task<List<Suggestion>> GetSuggestions(string search);
        Task<List<ProductModel>> GetSearchBarProduct(string searchtext);

        Task<OrderCommonResponse> AddToCart(CartItemDto cartItem);
        Task<OrderCommonResponse> UpdateCartItems(CartItemDto cartItemDto);
        Task<OrderCommonResponse> RemoveProductFromCart(int userId, int productId);
        Task<CartResponse> GetCartItemsByUserID(int userId);
        Task<string> UpdateCartCount(int userId);
        Task<OrderCommonResponse> CreateOrder(OrderRequest orderRequest);
    }
}
