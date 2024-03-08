using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.HCSN2.DUONG.BL;
using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.Common.Enums;
using MISA.WEB07.HCSN2.DUONG.NTier.Base;

namespace MISA.WEB07.HCSN.DUONG.NTier
{
    [Authorize]
    public class PropertyTypesController : BasesController<PropertyType>
    {
        #region Field

        private IPropertyTypeBL _propertyTypeBL;

        #endregion

        #region Constructor

        public PropertyTypesController(IPropertyTypeBL propertyTypeBL) : base(propertyTypeBL)
        {
            _propertyTypeBL = propertyTypeBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>

        [HttpGet]
        public override IActionResult GetAllRecords()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _propertyTypeBL.GetAllRecords());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        #endregion
    }
}
