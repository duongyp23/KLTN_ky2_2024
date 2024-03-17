﻿using System;
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
using KLTN.Common.Entity.DTO;
using System.Security.Cryptography;
using System.Web.Helpers;

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
        public async Task<object> Authenticate(LoginForm user)
        {
            List<Filter> filters = new List<Filter>()
            {
                new Filter()
                {
                    columnName = nameof(User.phone_number),
                    filterValue = user.Account,
                    operatorValue = "=",
                }
            };
            List<User> users = await _userDL.GetDataByField(filters);
            if (users.Count == 1)
            {
                if (Crypto.VerifyHashedPassword(users[0].password, user.Password))
                {
                    return new
                    {
                        token = generateJwtToken(users[0]),
                        role = users[0].is_manager,
                        user_name = users[0].user_name
                    };
                } else
                {
                    return null;
                }
            } else
            {
                return null;
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
                new Claim("Account", user.phone_number.ToString()),
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
            return new JwtSecurityTokenHandler().WriteToken(token) ;
        }

        public async Task<object> Register(User user)
        {
            user.password = Crypto.HashPassword(user.password);
            user.is_manager = 0;
            Guid id = await _userDL.Insert(user); 
            if(id != Guid.Empty)
            {
                return new {
                    token = generateJwtToken(user),
                    role = 0,
                    user_name = user.user_name
                };
            }
            return null;
        }

        #endregion
    }
}
