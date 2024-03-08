﻿using System;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.HCSN2.DUONG.BL;
using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.Common.Enums;
using MISA.WEB07.HCSN2.DUONG.NTier.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace MISA.WEB07.HCSN.DUONG.NTier
{
   
    public class UsersController : BasesController<User>
    {
        #region Field

        private IUserBL _userBL;

        #endregion

        #region Constructor

        public UsersController(IUserBL userBL) : base(userBL)
        {
            _userBL = userBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>

        [HttpGet("all-users")]
        public override IActionResult GetAllRecords()
        {
            
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            
        }

        /// <summary>
        /// Đăng nhập tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult Authenticate([FromBody] User user)
        {
            try
            {
                var token = _userBL.Authenticate(user);
                return StatusCode(StatusCodes.Status200OK, token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        #endregion
    }
}
