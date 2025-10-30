using StoreCoreApi.DAL.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Interface
{
    public interface IProduct
    {
        Task<CommonResponse> AddProduct(insertProduct Request);
        Task<List<ProductModel>> GetALLProduct();
        Task<ProductModel> GetProductById(int productId);
        Task<List<ProductModel>> SearchProductbyFilter(SearchProduct searchProduct);


        Task<List<ProductModel>> GetWishlistAsync(int userId);
        Task<CommonResponse> AddToWishlistAsync(WishlistReq wishlistReq);
        Task<CommonResponse> RemoveFromWishlistAsync(WishlistReq wishlistReq);

        Task<List<Suggestion>> GetSuggestions(string search);
        Task<List<ProductModel>> GetSearchBarProduct(string searchtext);
    }
}
