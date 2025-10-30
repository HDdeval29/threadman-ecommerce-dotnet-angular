using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Model.Masters
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class Brands
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
    public class Sizes
    {
        public int SizeId { get; set; }
        public string SizeName { get; set; }
    }
    public class Colours
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
    }
    public class FitTypes
    {
        public int FitTypeId { get; set; }
        public string FitTypeName { get; set; }
    }
    public class Gender
    {
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }
    public class CommonMastersResponse
    {

    }
}
