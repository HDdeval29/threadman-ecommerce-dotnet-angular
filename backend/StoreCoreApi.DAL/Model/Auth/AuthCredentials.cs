using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Model.Auth
{
    public class UserCredential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthUser //: UserCredential
    {
        public string Token { get; set; }
    }
}
