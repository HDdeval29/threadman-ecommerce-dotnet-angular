using StoreCoreApi.DAL.Interface;
using StoreCoreApi.DAL.Model.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Repository
{
    public class MasterServices : IMasterServices
    {
        private readonly IDbServices _dbServices;
        public MasterServices(IDbServices dbServices)
        {
            _dbServices = dbServices;
        }

        public async Task<List<Category>> GetCategoryList()
        {
            DataTable dt = new DataTable();
            var query = "SELECT * FROM Category";

            dt = await _dbServices.ExecuteQueryAsync(query);

            var Response = dt.AsEnumerable().Select(row =>
                         new Category
                         {
                             CategoryId = Convert.ToInt32(row["CategoryId"]),
                             CategoryName = row["CategoryName"]?.ToString()
                         }).ToList();

            return Response;
        }

        public async Task<List<Brands>> GetBrandsList()
        {
            //var response = new CommonMastersResponse();
            //var list = new List<Brands>();
            DataTable dt = new DataTable();
            var query = "SELECT * FROM Brands";

            dt = await _dbServices.ExecuteQueryAsync(query);

            var Response = dt.AsEnumerable().Select(row =>
                         new Brands
                         {
                             BrandId = Convert.ToInt32(row["BrandId"]),
                             BrandName = row["BrandName"]?.ToString()
                         }).ToList();

            //foreach (DataRow row in dt.Rows)
            //{
            //    var item = new Brands();
            //    foreach (var prop in typeof(Brands).GetProperties())
            //    {
            //        if (dt.Columns.Contains(prop.Name))
            //        {
            //            var value = row[prop.Name];
            //            if (value != DBNull.Value)
            //            {
            //                prop.SetValue(item, Convert.ChangeType(value, prop.PropertyType), null);
            //            }
            //        }
            //    }
            //    list.Add(item);
            //}

            return Response;
        }

        public async Task<List<Sizes>> GetSizesList()
        {
            DataTable dt = new DataTable();
            var query = "SELECT * FROM Sizes";

            dt = await _dbServices.ExecuteQueryAsync(query);

            var Response = dt.AsEnumerable().Select(row =>
                         new Sizes
                         {
                             SizeId = Convert.ToInt32(row["SizeId"]),
                             SizeName = row["SizeName"]?.ToString()
                         }).ToList();

            return Response;
        }
        public async Task<List<FitTypes>> GetFitTypesList()
        {
            DataTable dt = new DataTable();
            var query = "SELECT * FROM FitTypes";

            dt = await _dbServices.ExecuteQueryAsync(query);

            var Response = dt.AsEnumerable().Select(row =>
                         new FitTypes
                         {
                             FitTypeId = Convert.ToInt32(row["FitTypeId"]),
                             FitTypeName = row["FitTypeName"]?.ToString()
                         }).ToList();

            return Response;
        }
        public async Task<List<Colours>> GetColoursList()
        {
            DataTable dt = new DataTable();
            var query = "SELECT * FROM Colors";

            dt = await _dbServices.ExecuteQueryAsync(query);

            var Response = dt.AsEnumerable().Select(row =>
                         new Colours
                         {
                             ColorId = Convert.ToInt32(row["ColorId"]),
                             ColorName = row["ColorName"]?.ToString()
                         }).ToList();

            return Response;

    }

    public async Task<List<Gender>> GetGenderList()
    {
        DataTable dt = new DataTable();
        var query = "SELECT * FROM Gender";

        dt = await _dbServices.ExecuteQueryAsync(query);

        var Response = dt.AsEnumerable().Select(row =>
                     new Gender
                     {
                         GenderId = Convert.ToInt32(row["GenderId"]),
                         GenderName = row["GenderName"]?.ToString()
                     }).ToList();

        return Response;
    }
}
}
