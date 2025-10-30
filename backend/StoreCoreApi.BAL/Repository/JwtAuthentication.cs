using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StoreCoreApi.BAL.Interface;
using StoreCoreApi.DAL.Model.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoreCoreApi.BAL.Repository
{
    public class JwtAuthentication: IJwtAuthentication
    {
        //private readonly string username = "Hddeval@29";
        //private readonly string password = "Deval@29";

        private readonly string _username;
        private readonly string _password;
        private readonly string _secretKey;

        IConfiguration _configuration;

        //private readonly string key;
        public JwtAuthentication(IConfiguration configuration) //string key
        {
            _configuration = configuration;
            //this.key = key;

            _username = _configuration["Authentication:Username"];
            _password = _configuration["Authentication:Password"];
            _secretKey = _configuration["Authentication:SecretKey"];
        }
        public AuthUser GenerateToken(string username, string password)
        {
            if (!username.Equals(_username) && !password.Equals(_password))
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(_secretKey); //(key)

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity( 
                    new Claim[] {
                        new Claim(ClaimTypes.Name, username)
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);


            // 5. Return Token from method
            //return tokenHandler.WriteToken(token);

            var authUser = new AuthUser();

            authUser.Token = tokenHandler.WriteToken(token); 

            return authUser;
        }
    }
}
