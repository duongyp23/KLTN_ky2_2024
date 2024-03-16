using KLTN.BussinesLayer;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using KLTN.Common.Enums;
using KLTN.NTier.Base;
using Microsoft.AspNetCore.Mvc;
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
        /// Đăng nhập tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult Authenticate([FromBody] LoginForm user)
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

        /// <summary>
        /// Đăng nhập tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async  Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                object token = await _userBL.Register(user);
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
