using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.AdminPortal
{
    public class AdminCommonResponse
    {
        public int UserId { get; set; }
        public string Result { get; set; }
        public string ErrorMsg { get; set; }
        public bool ErrorStatus { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }

    }
    public class AdminLogin
    {
        public int ID { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Productinsert
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string ImageUrl { get; set; }
        public string BrandId { get; set; }
        public string ColorId { get; set; }
        public string FitTypeId { get; set; }
        public string SizeId { get; set; }
    }
}
