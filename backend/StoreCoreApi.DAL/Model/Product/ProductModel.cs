using StoreCoreApi.DAL.Model.UserCredentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Model.Product
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } // Can be 'Men', 'Women', or 'Kids'
        public string SubCategory { get; set; }
        public string ImageUrl { get; set; }

        public byte[] imageArray { get; set; }
        public string base64_ImgPath { get; set; }

        public string BrandName { get; set; }
        public string ColorNames { get; set; }
        public string FitTypeName { get; set; }
        public string Sizes { get; set; }
        //public string Gender { get; set; }
    }

    public class ProductCategory
    {
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }

    public class CommonResponse
    {
        public string Error { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
    }
    public class insertProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string ImageUrl { get; set; }
        public string BrandName { get; set; }
        public string ColorNames { get; set; }
        public string FitTypeName { get; set; }
        public string Sizes { get; set; }
    }

    //public class SearchProduct
    //{
    //    public string Category { get; set; }
    //    public string SubCategory { get; set; }
    //    public string BrandName { get; set; }
    //    public string ColorNames { get; set; }
    //    public string FitTypeName { get; set; }
    //    public string Sizes { get; set; }
    //}
    public class SearchProduct
    {
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Brand { get; set; } // Changed from BrandName to Brand
        public string Color { get; set; } // Changed from ColorNames to Color
        public string FitType { get; set; } // Changed from FitTypeName to FitType
        public string Size { get; set; } // Changed from Sizes to Size
    }

    public class Wishlist
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public UserLoginData User { get; set; }
        public ProductModel Product { get; set; }
    }

    public class WishlistReq
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }

    public class Suggestion
    {
        public string Label { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
    public class SearchBarProduct
    {
        public string SearchText { get; set; }
    }

}
