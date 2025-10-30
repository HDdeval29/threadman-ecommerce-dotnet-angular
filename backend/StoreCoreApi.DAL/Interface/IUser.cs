using StoreCoreApi.DAL.Model.UserCredentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.DAL.Interface
{
    public interface IUser
    {
        Task<ReturnResponse> InsertUserDetail(UserRegisterData userRegisterData);
        Task<ReturnUserData> UserLogin(UserLoginData userLoginData);
        Task<ReturnResponse> ForgotUserNameOrPassword(forgotPassword request);
    }
}
