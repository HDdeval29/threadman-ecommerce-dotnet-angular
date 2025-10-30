using StoreCoreApi.DAL.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Model.UserCredentials
{
    public class UserRegisterData
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
    }
    public class UserLoginData
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ReturnUserData 
    {
        public int UserId { get; set; }
        public string ErrorMsg { get; set; }
        public bool ErrorStatus { get; set; }
        public string Result { get; set; }
        public string UserFullName { get; set; }
        public string UserMobileNo { get; set; }
        public string UserEmail { get; set; }
        public string SavedAddresses { get; set; }
        public string OrderHistory { get; set; }
        public string CartCount { get; set; }

        public UserProfileWithAddresses userProfileWithAddresses { get; set; }
        public List<UserOrders> userOrders { get; set; }
    }

    public class forgotPassword
    {
        public int UserId { get; set; }
        public string Type { get; set; }
        public string NewPassword { get; set; }
        public string EmailOrMobileNumber { get; set; }
    }
    public class ReturnResponse
    {
        public int UserId { get; set; }
        public string ErrorMsg { get; set; }
        public bool ErrorStatus { get; set; }
        public string Result { get; set; }
        public string Otp { get; set; }
    }

    public class UserProfileWithAddresses
    {
        public int ProfileID { get; set; }
        public int UserID { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string Preferences { get; set; }

        // List of user addresses
        public List<UserAddress> Addresses { get; set; } = new List<UserAddress>();
    }

    public class UserAddress
    {
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public string RecipientName { get; set; }
        public string RecipientContactNo { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public bool IsDefault { get; set; }
    }

    public class UserOrders: ProductModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderStatus { get; set; }
        public string FormattedOrderId { get; set; }
    }

}
