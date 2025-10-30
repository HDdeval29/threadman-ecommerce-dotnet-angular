using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.AdminPortal
{
    public interface IAdmin
    {
        Task<AdminCommonResponse> AdminLogin(AdminLogin request);
        Task<AdminCommonResponse> InsertProduct(Productinsert Request);
    }
}
