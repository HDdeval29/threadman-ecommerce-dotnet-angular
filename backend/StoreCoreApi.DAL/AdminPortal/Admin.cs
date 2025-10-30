using StoreCoreApi.DAL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.AdminPortal
{
    public class Admin: IAdmin
    {
        private readonly IDbServices _dbServices;
        public Admin(IDbServices dbServices)
        {
            _dbServices = dbServices;
        }

        public async Task<AdminCommonResponse> AdminLogin(AdminLogin request)
        {
            var res = new AdminCommonResponse();
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable();
                ht.Add("Username", request.UserName);
                ht.Add("Password", request.Password);
                //ht.Add("Role", request.Role);

                ds = await _dbServices.DBase<DataSet>("SpAdminLogin", ht);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string Msg = Convert.ToString(ds.Tables[1].Rows[0]["LoginStatus"]);
                    switch (Msg)
                    {
                        case "Success":
                            res.Result = "Login Successfully";
                            //res.UserId = Convert.ToInt32(ds.Tables[1].Rows[0]["ID"]);
                            //res.AdminName = ds.Tables[1].Rows[0]["FullName"].ToString();  //ds.Tables[0].Rows[0]["userID"].ToString();
                            break;
                        default:
                            res.ErrorStatus = true;
                            res.ErrorMsg = "Failed";
                            res.Result = "Failed";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorStatus =true;
                res.ErrorMsg = "Login Failed" + ex.Message.ToString();
            }
            return res;
        }


        public async Task<AdminCommonResponse> InsertProduct(Productinsert Request)
        {
            var Response = new AdminCommonResponse();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@Name", Request.Name);
                ht.Add("@Description", Request.Description);
                ht.Add("@Price", Request.Price);
                ht.Add("@Category", Request.Category);
                ht.Add("@SubCategory", Request.SubCategory);
                ht.Add("@ImageUrl", Request.ImageUrl);
                ht.Add("@BrandId", Request.BrandId);
                ht.Add("@ColorId", Request.ColorId);
                ht.Add("@FitTypeId", Request.FitTypeId);
                ht.Add("@SizeId", Request.SizeId);
                //ht.Add("@GenderName", Request.Gender);
                //ht.Add("@ProductId", "");

                DataTable dt = await _dbServices.DBase<DataTable>("SpInsertProducts", ht);
                string ProductId = Convert.ToString(dt.Rows[0][0]);
                //Response.ErrorStatus = "Success";
                Response.Result = "ProductID:-" + ProductId + " - " + "Add Product Successfully";
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                Response.ErrorStatus = true;
                Response.ErrorMsg = error;
            }
            return Response;
        }

        public static (string Hash, string Salt) HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                var salt = Convert.ToBase64String(saltBytes);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
                {
                    var hashBytes = pbkdf2.GetBytes(20);
                    var hash = Convert.ToBase64String(hashBytes);
                    return (Hash: hash, Salt: salt);
                }
            }
        }

        //When a user changes their password, you need to verify the old password before allowing the change. Here’s how you use VerifyPassword in this context:
        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                var hashBytes = pbkdf2.GetBytes(20);
                var hash = Convert.ToBase64String(hashBytes);

                return hash == storedHash;
            }
        }
    }
}
