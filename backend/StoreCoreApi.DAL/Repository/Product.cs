using StoreCoreApi.DAL.Interface;
using StoreCoreApi.DAL.Model.Product;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Repository
{
    public class Product : IProduct
    {
        private readonly List<ProductModel> _products;

        private readonly IDbServices _dbServices;
        private object row;

        public Product(IDbServices dbServices)
        {

            _dbServices = dbServices;
        }

        public static string ConvertImageToBase64(string imagePath)
        {
            string base64Image = null;
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                return null;

            //byte[] imageBytes = File.ReadAllBytes(imagePath);
            //return Convert.ToBase64String(imageBytes);

            try
            {
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                base64Image = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                //return Convert.ToBase64String(imageBytes);
                return base64Image;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading image file: {ex.Message}");
                return null;
            }
        }

        public async Task<CommonResponse> AddProduct(insertProduct Request)
        {
            var Response = new CommonResponse();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@Name", Request.Name);
                ht.Add("@Description", Request.Description);
                ht.Add("@Price", Request.Price);
                ht.Add("@Category", Request.Category);
                ht.Add("@SubCategory", Request.SubCategory);
                ht.Add("@ImageUrl", Request.ImageUrl);
                ht.Add("@BrandName", Request.BrandName);
                ht.Add("@ColorName", Request.ColorNames);
                ht.Add("@FitTypeName", Request.FitTypeName);
                ht.Add("@SizeName", Request.Sizes);
                //ht.Add("@GenderName", Request.Gender);
                //ht.Add("@ProductId", "");

                DataTable dt = await _dbServices.DBase<DataTable>("SpInsertProduct", ht);
                string ProductId = Convert.ToString(dt.Rows[0][0]);
                Response.Status = "Success";
                Response.Result = "ProductID:-" + ProductId + " - " + "Add Product Successfully";
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                Response.Status = "Failed";
                Response.Error = error;
            }
            return Response;
        }
        public async Task<List<ProductModel>> GetALLProduct()
        {
            var productList = new List<ProductModel>();
            try
            {
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable();

                dt = await _dbServices.DBase<DataTable>("SpGetAllProducts", ht);

                // Convert DataTable to List<ProductModel>
                foreach (DataRow row in dt.Rows)
                {
                    var imagePath = row["ImageUrl"].ToString();
                    string base64Image = null;

                    // Check if the image path is not empty and the file exists
                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                        base64Image = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                    }
                    var product = new ProductModel
                    {
                        //Id = Convert.ToInt32(row["Id"]),
                        ProductId = row["ProductId"].ToString(),
                        Name = row["Name"].ToString(),
                        Description = row["Description"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Category = row["Category"].ToString(),
                        SubCategory = row["SubCategory"].ToString(),
                        ImageUrl = base64Image,
                        BrandName = row["BrandName"].ToString(),
                        Sizes = row["SizeName"].ToString(),
                        ColorNames = row["ColorName"].ToString(),
                        //Gender = row["GenderName"].ToString(),
                        FitTypeName = row["FitTypeName"].ToString()
                        //ImageUrl = row["ImageUrl"].ToString(),
                        //base64_ImgPath = "data:image/png;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(row["ImageUrl"].ToString()))

                    };
                    productList.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return productList;
        }
        public async Task<ProductModel> GetProductById(int productId)
        {
            ProductModel product = null;
            try
            {
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable
                {
                    { "@ProductId", productId }
                };

                dt = await _dbServices.DBase<DataTable>("SpGetProductById", ht);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    var imagePath = row["ImageUrl"].ToString();
                    string base64Image = null;

                    // Check if the image path is not empty and the file exists
                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                        base64Image = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                    }

                    product = new ProductModel
                    {
                        //Id = Convert.ToInt32(row["Id"]),
                        //ProductId = row["ProductId"].ToString(),
                        //Name = row["Name"].ToString(),
                        //Description = row["Description"].ToString(),
                        //Price = Convert.ToDecimal(row["Price"]),
                        //Category = row["Category"].ToString(),
                        //SubCategory = row["SubCategory"].ToString(),
                        //ImageUrl = base64Image


                        ProductId = row["ProductId"].ToString(),
                        Name = row["Name"].ToString(),
                        Description = row["Description"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Category = row["Category"].ToString(),
                        SubCategory = row["SubCategory"].ToString(),
                        ImageUrl = base64Image,
                        BrandName = row["BrandName"].ToString(),
                        Sizes = row["SizeName"].ToString(),
                        ColorNames = row["ColorName"].ToString(),
                        //Gender = row["GenderName"].ToString(),
                        FitTypeName = row["FitTypeName"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return product;
        }


        public async Task<List<ProductModel>> SearchProductbyFilter(SearchProduct searchProduct)
        {
            var productList = new List<ProductModel>();
            try
            {
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable();

                ht.Add("@Category", searchProduct.Category);
                ht.Add("@SubCategory", searchProduct.SubCategory);
                //ht.Add("@GenderName", searchProduct.Gender);
                ht.Add("@BrandName", searchProduct.Brand);
                ht.Add("@FitTypeName", searchProduct.FitType);
                ht.Add("@SizeName", searchProduct.Size);
                ht.Add("@ColorName", searchProduct.Color);

                dt = await _dbServices.DBase<DataTable>("SpSearchFilterProducts", ht);

                //foreach (DataRow row in dt.Rows)
                //{
                //    var SearchedProduct = new ProductModel
                //    {

                //    }
                //}

                var response = dt.AsEnumerable().Select(row =>
                         new ProductModel
                         {
                             ProductId = row["ProductId"].ToString(),
                             Name = row["Name"].ToString(),
                             Description = row["Description"].ToString(),
                             Price = Convert.ToDecimal(row["Price"]),
                             Category = row["Category"].ToString(),
                             SubCategory = row["SubCategory"].ToString(),
                             ImageUrl = ConvertImageToBase64(row["ImageUrl"].ToString()),
                             BrandName = row["BrandName"].ToString(),
                             Sizes = row["SizeName"].ToString(),
                             ColorNames = row["ColorName"].ToString(),
                             //Gender = row["GenderName"].ToString(),
                             FitTypeName = row["FitTypeName"].ToString()
                         }).ToList();

                productList = response;
            }
            catch (Exception ex)
            {
                string Error = ex.Message.ToString();
            }
            return productList;
        }

        

        public async Task<List<ProductModel>> GetProductsByCategory(string category, string subCategory = null)
        {
            var productList = new List<ProductModel>();
            try
            {
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable
                {
                    { "@Category", category }
                };

                // Add SubCategory parameter only if it's not null
                if (!string.IsNullOrEmpty(subCategory))
                {
                    ht.Add("@SubCategory", subCategory);
                }
                else
                {
                    ht.Add("@SubCategory", DBNull.Value);
                }

                dt = await _dbServices.DBase<DataTable>("SpGetProductsByCategory", ht);

                foreach (DataRow row in dt.Rows)
                {
                    var imagePath = row["ImageUrl"].ToString();
                    string base64Image = null;

                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                        base64Image = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                    }

                    var product = new ProductModel
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        ProductId = row["ProductId"].ToString(),
                        Name = row["Name"].ToString(),
                        Description = row["Description"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Category = row["Category"].ToString(),
                        SubCategory = row["SubCategory"].ToString(),
                        ImageUrl = base64Image
                    };
                    productList.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return productList;
        }



        public async Task<List<ProductModel>> GetWishlistAsync(int userId)
        {
            var parameters = new Hashtable { { "UserId", userId } };
            DataTable dt = await _dbServices.DBase<DataTable>("SpGetWishlist", parameters);

            var products = new List<ProductModel>();
            foreach (DataRow row in dt.Rows)
            {
                var imagePath = row["ImageUrl"].ToString();
                string base64Image = null;

                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                    base64Image = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                }
                products.Add(new ProductModel
                {
                    ProductId = Convert.ToString(row["ProductId"]),
                    Name = Convert.ToString(row["Name"]),
                    Description = Convert.ToString(row["Description"]),
                    Price = Convert.ToDecimal(row["Price"]),
                    ImageUrl = base64Image
                });
            }

            return products;
        }

        public async Task<CommonResponse> AddToWishlistAsync(WishlistReq wishlistReq)
        {
            var response = new CommonResponse();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("UserId", wishlistReq.UserId);
                ht.Add("ProductId", wishlistReq.ProductId);

                DataTable dt = await _dbServices.DBase<DataTable>("SpAddToWishlist", ht);

                if (dt.Rows.Count > 0)
                {
                    response.Status = "failed";
                    response.Error = Convert.ToString(dt.Rows[0]["Message"]);
                }
                else
                {
                    response.Status = "Success";
                    response.Result = "Successfully Added";
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }


        public async Task<CommonResponse> RemoveFromWishlistAsync(WishlistReq wishlistReq)
        {
            var response = new CommonResponse();
            try
            {
                var parameters = new Hashtable
            {
                { "UserId", wishlistReq.UserId },
                { "ProductId", wishlistReq.ProductId }
            };

                DataTable rowsAffected = await _dbServices.DBase<DataTable>("SpRemoveFromWishlist", parameters);

                if (rowsAffected.Rows.Count > 0)
                {
                    response.Status = "Success";
                    response.Result = "Successfully Removed";
                }
                else
                {
                    response.Status = "Failed";
                    response.Error = "Product not found in wishlist.";
                }
            }
            catch (Exception ex)
            {
                response.Status = "Error";
                response.Error = $"Error: {ex.Message}";
            }
            return response;
        }


        public async Task<List<Suggestion>> GetSuggestions(string search)
        {
            List<Suggestion> suggestions = new List<Suggestion>();
            try
            {
                DataTable dt = new DataTable();

                string query = @" SELECT Label, Category, Brand, Type FROM Suggestions 
                                  WHERE Label LIKE @search OR Brand LIKE @search
                                  ORDER BY Label
                                  OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY"; // Limit results to 10

                var parameters = new[]
                {
                    new SqlParameter("@search", "%" + search + "%")
                };

                dt = await _dbServices.ExecuteQueryAsync2(query, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    suggestions.Add(new Suggestion
                    {
                        Label = row["Label"].ToString(),
                        Category = row["Category"].ToString(),
                        Brand = row["Brand"].ToString(),
                        Type = row["Type"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Internal server error: " + ex.Message);
            }
            return suggestions;
        }

        public async Task<List<ProductModel>> GetSearchBarProduct(string searchtext)
        {
            var productList = new List<ProductModel>();
            try
            {
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable();
                string SearchText1 = string.Empty;
                if (searchtext == "Men's Jeans" || searchtext == "Women's Jeans")
                {
                    string[] text = searchtext.Split("'s");

                    searchtext = text[0];
                    SearchText1 = text[1].Trim();

                }
                ht.Add("@SearchText1", searchtext);
                ht.Add("@SearchText2", SearchText1);

                dt = await _dbServices.DBase<DataTable>("SpSearchbarProducts", ht);

                var response = dt.AsEnumerable().Select(row =>
                         new ProductModel
                         {
                             ProductId = row["ProductId"].ToString(),
                             Name = row["Name"].ToString(),
                             Description = row["Description"].ToString(),
                             Price = Convert.ToDecimal(row["Price"]),
                             Category = row["Category"].ToString(),
                             SubCategory = row["SubCategory"].ToString(),
                             ImageUrl = ConvertImageToBase64(row["ImageUrl"].ToString()),
                             BrandName = row["BrandName"].ToString(),
                             Sizes = row["SizeName"].ToString(),
                             ColorNames = row["ColorName"].ToString(),
                             FitTypeName = row["FitTypeName"].ToString()
                         }).ToList();

                productList = response;
            }
            catch (Exception ex)
            {
                string Error = ex.Message.ToString();
            }
            return productList;
        }
        
    }
}
