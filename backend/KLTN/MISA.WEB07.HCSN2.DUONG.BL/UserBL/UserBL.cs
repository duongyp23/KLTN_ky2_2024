using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using KLTN.Common.Entity;
using KLTN.DataLayer;

namespace KLTN.BussinesLayer
{

    public class UserBL : BaseBL<User>, IUserBL
    {
        #region Field

        private IUserDL _userDL;
        public IConfiguration _configuration;



        #endregion

        #region Constructor

        public UserBL(IUserDL userDL, IConfiguration configuration) : base(userDL)
        {
            _userDL = userDL;
            _configuration = configuration;
        }
        /// <summary>
        /// Chức năng đăng nhập
        /// NTD 2/10/2022
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string Authenticate(User user)
        {
            if (user.Account == "admin" && user.Password == "12345678" )
            {
                // authentication successful so generate jwt token
                var token = generateJwtToken(user);

                return token;
            } 
            else
            {
                throw new Exception("Tài khoản và mật khẩu không đúng");
            }
        }
        /// <summary>
        /// Tạo một JWT khi đăng nhập đúng
        /// NTD 2/10/2022
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string generateJwtToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Account", user.Account.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signIn
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
