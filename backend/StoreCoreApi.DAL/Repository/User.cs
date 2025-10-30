using StoreCoreApi.DAL.Interface;
using StoreCoreApi.DAL.Model.UserCredentials;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Repository
{
    public class User : IUser
    {

        private readonly IDbServices _dbServices;
        public User(IDbServices dbServices)
        {
            _dbServices = dbServices;
        }

        public async Task<ReturnResponse> InsertUserDetail(UserRegisterData userRegisterData)
        {
            var res = new ReturnResponse();
            try
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("UserName", userRegisterData.UserName);
                ht.Add("FullName", userRegisterData.FullName);
                ht.Add("Email", userRegisterData.Email);
                ht.Add("MobileNo", userRegisterData.MobileNo);
                ht.Add("Password", userRegisterData.Password);

                dt = await _dbServices.DBase<DataTable>("SpInsertUser", ht);

                if (dt.Rows.Count > 0)
                {
                    string Msg = Convert.ToString(dt.Rows[0]["Message"]);
                    if (Msg == "0")
                    {
                        //res.UserId = Convert.ToString(dt.Rows[0]["Id"]);
                        res.Result = "User Already exists";
                    }
                    else
                    {
                        res.UserId = Convert.ToInt32(dt.Rows[0]["Message"]);
                        res.Result = "Registration successfully";
                    }
                }
                else
                {
                    res.ErrorStatus = true;
                    res.ErrorMsg = "Registration Failed";
                    res.Result = "Failed";
                }
            }
            catch (Exception ex)
            {
                res.ErrorStatus = true;
                res.ErrorMsg = "Registration Failed" + ex.Message.ToString();
            }
            return res;
        }

        public async Task<ReturnUserData> UserLogin(UserLoginData userLoginData)
        {
            var res = new ReturnUserData();
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable();

                ht.Add("UserName", userLoginData.UserName);
                ht.Add("Password", userLoginData.Password);

                //dt = await _dbServices.DBase<DataTable>("SpGetUser", ht);

                ds = await _dbServices.DBase<DataSet>("SpGetUser", ht);
                //ds.Tables[0].Rows.Count > 0
                    if (ds.Tables[0].Rows.Count > 0)
                {
                    string Msg = Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                    switch (Msg)
                    {
                        case "Valid User":
                            res.Result = "Login Successfully";
                            res.UserId = Convert.ToInt32(ds.Tables[1].Rows[0]["UserId"]);
                            res.UserFullName = ds.Tables[1].Rows[0]["FullName"].ToString();  //ds.Tables[0].Rows[0]["userID"].ToString();

                            ht.Clear();
                            ht.Add("@UserId", res.UserId);
                            dt = await _dbServices.DBase<DataTable>("SpCartCount", ht);
                            if (dt.Rows.Count > 0)
                            {
                                res.CartCount = !string.IsNullOrEmpty(dt.Rows[0]["CartCount"].ToString()) ? dt.Rows[0]["CartCount"].ToString() : "";
                            }
                            UserProfileWithAddresses userProfileWithAddresses = new UserProfileWithAddresses();
                            userProfileWithAddresses = await GetUserProfileWithAddresses(res.UserId);
                            res.userProfileWithAddresses = userProfileWithAddresses;
                            res.userOrders = await GetUserOrders(res.UserId);
                            break;
                        case "Invalid User":
                            res.ErrorStatus = true;
                            res.ErrorMsg = "Invalid Username";
                            res.Result = "Failed";
                            break;
                        default:
                            res.ErrorStatus = true;
                            res.ErrorMsg = "Invalid Password";
                            res.Result = "Failed";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorStatus = true;
                res.ErrorMsg = "Login Failed" + ex.Message.ToString();
            }
            return res;
        }

        public async Task<ReturnResponse> ForgotUserNameOrPassword(forgotPassword request)
        {
            var res = new ReturnResponse();
            try
            {
                DataTable dt = new DataTable();
                Hashtable ht = new Hashtable();

                

                if (request.Type == "OtpSent")
                {
                    ht.Add("Id", request.UserId);

                    dt = await _dbServices.DBase<DataTable>("SpGetUserDetails", ht);

                    string UserEmail = Convert.ToString(dt.Rows[0]["Email"]);
                    string UserMobile = Convert.ToString(dt.Rows[0]["MobileNo"]);

                    if (UserEmail == request.EmailOrMobileNumber || UserMobile == request.EmailOrMobileNumber)
                    {
                        Random random = new Random();
                        int length = 6;
                        const string digits = "0123456789";
                        char[] otp = new char[length];
                        for (int i = 0; i < length; i++)
                        {
                            otp[i] = digits[random.Next(digits.Length)];
                        }
                      
                        res.Otp = new string(otp);  
                        return res;       
                    }
                    else
                    {
                        res.ErrorMsg = "Provide Registerd Email or Moblie Number";
                    }
                }
                else if (request.Type == "update Password")
                {
                   
                    ht.Add("EmailOrMobileNumber", request.EmailOrMobileNumber);
                    ht.Add("NewPassword", request.NewPassword);
              
                    dt = await _dbServices.DBase<DataTable>("SpForgetPassword", ht);
             
                    res.Result = "Your password has been updated successfully";
                    ht.Clear();
                }
            }
            catch (Exception ex)
            {
                res.ErrorStatus = true;
                res.ErrorMsg = "Error" + ex.Message.ToString();
            }
            return res;
        }


        public async Task<UserProfileWithAddresses> GetUserProfileWithAddresses(int userId)
        {
            var profileWithAddresses = new UserProfileWithAddresses();
            var addresses = new List<UserAddress>();

            DataTable dt = new DataTable();
            Hashtable ht = new Hashtable();

            ht.Add("@UserID", userId);

            dt = await _dbServices.DBase<DataTable>("SpGetUserProfileWithAddresses", ht);


            if (dt.Rows.Count > 0)
            {
                // Assuming the first row contains the profile information
                var row = dt.Rows[0];

                profileWithAddresses = new UserProfileWithAddresses
                {
                    ProfileID = (int)row["ProfileID"],
                    UserID = (int)row["UserID"],
                    BillingAddress = row["BillingAddress"].ToString(),
                    ShippingAddress = row["ShippingAddress"].ToString(),
                    City = row["ProfileCity"].ToString(),
                    State = row["ProfileState"].ToString(),
                    ZipCode = row["ProfileZipCode"].ToString(),
                    Country = row["ProfileCountry"].ToString(),
                    ProfilePicture = row["ProfilePicture"].ToString(),
                    DateOfBirth = (DateTime)row["DateOfBirth"],
                    Bio = row["Bio"].ToString(),
                    Preferences = row["Preferences"].ToString(),
                    Addresses = addresses // Initialize empty list for addresses
                };

                // Iterate through remaining rows for addresses
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var addressRow = dt.Rows[i];
                    if (addressRow["AddressID"] != DBNull.Value) // Check if AddressID exists
                    {
                        addresses.Add(new UserAddress
                        {
                            AddressID = (int)addressRow["AddressID"],
                            RecipientName = addressRow["RecipientName"].ToString(),
                            RecipientContactNo = addressRow["RecipientContactNo"].ToString(),
                            AddressLine1 = addressRow["AddressLine1"].ToString(),
                            AddressLine2 = addressRow["AddressLine2"].ToString(),
                            Landmark = addressRow["Landmark"].ToString(),
                            City = addressRow["AddressCity"].ToString(),
                            State = addressRow["AddressState"].ToString(),
                            ZipCode = addressRow["AddressZipCode"].ToString(),
                            Country = addressRow["AddressCountry"].ToString(),
                            IsDefault = (bool)addressRow["IsDefault"]
                        });
                    }
                }
            }

            profileWithAddresses.Addresses = addresses; // Assign the addresses to the profile
            return profileWithAddresses;
        }

        public async Task<List<UserOrders>> GetUserOrders(int userId)
        {
            var orders = new List<UserOrders>();
            try
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();

                ht.Add("@UserId",userId);

                dt = await _dbServices.DBase<DataTable>("SpGetOrdersByUserId", ht);
                
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var imagePath = row["ImageUrl"].ToString();
                        string base64Image = null;

                        if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                        {
                            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                            base64Image = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                        }

                        var order = new UserOrders
                        {
                            Id = row["OrderId"] != DBNull.Value ? Convert.ToInt32(row["OrderId"]) : 0,
                            //OrderId = row["FormattedOrderId"] != DBNull.Value ? Convert.ToInt32(row["FormattedOrderId"]) : 0,
                            OrderDetailId = row["OrderDetailId"] != DBNull.Value ? Convert.ToInt32(row["OrderDetailId"]) : 0,
                            OrderDate = row["OrderDate"] != DBNull.Value ? Convert.ToDateTime(row["OrderDate"]) : DateTime.MinValue,
                            TotalAmount = row["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(row["TotalAmount"]) : 0,
                            PaymentStatus = !string.IsNullOrEmpty(Convert.ToString(row["PaymentStatus"])) ? row["PaymentStatus"].ToString() :  string.Empty,
                            ShippingAddress = !string.IsNullOrEmpty(Convert.ToString(row["ShippingAddress"])) ? Convert.ToString(row["ShippingAddress"]) : string.Empty,
                            ImageUrl = base64Image,
                            ProductId = !string.IsNullOrEmpty(Convert.ToString(row["ProductId"])) ? Convert.ToString(row["ProductId"]) : string.Empty,
                            Name = !string.IsNullOrEmpty(Convert.ToString(row["Name"])) ? Convert.ToString(row["Name"]) : string.Empty,
                            Sizes = !string.IsNullOrEmpty(Convert.ToString(row["Size"])) ? Convert.ToString(row["Size"]) : string.Empty,
                            OrderStatus = row["OrderStatus"].ToString() ?? string.Empty,
                            FormattedOrderId = !string.IsNullOrEmpty(Convert.ToString(row["FormattedOrderId"])) ? Convert.ToString(row["FormattedOrderId"]) : string.Empty,
                        };

                        orders.Add(order);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching orders: {ex.Message}");
            }
            return orders;
        }
    }
}
