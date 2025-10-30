using StoreCoreApi.DAL.Model.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Interface
{
    public interface IMasterServices
    {
        Task<List<Category>> GetCategoryList();
        Task<List<Brands>> GetBrandsList();
        Task<List<Sizes>> GetSizesList();
        Task<List<FitTypes>> GetFitTypesList();
        Task<List<Colours>> GetColoursList();
        Task<List<Gender>> GetGenderList();
    }
}
