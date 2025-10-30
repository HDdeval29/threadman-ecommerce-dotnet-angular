using StoreCoreApi.DAL.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.BAL.Interface
{
    public interface IJwtAuthentication
    {
        //string GenerateToken(string username, string password);
        AuthUser GenerateToken(string username, string password);
    }
}
