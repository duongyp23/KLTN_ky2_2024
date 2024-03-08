using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.HCSN2.DUONG.BL;
using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.Common.Entity.DTO;
using MISA.WEB07.HCSN2.DUONG.Common.Enums;
using MISA.WEB07.HCSN2.DUONG.NTier.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace MISA.WEB07.HCSN.DUONG.NTier.Controllers
{
    [Authorize]
    [ApiController]
    public class VouchersController : BasesController<Voucher>
    {
        #region Field

        private IVoucherBL _voucherBL;

        #endregion

        #region Constructor

        public VouchersController(IVoucherBL voucherBL) : base(voucherBL)
        {
            _voucherBL = voucherBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>

        [HttpGet("all-record")]
        public override IActionResult GetAllRecords()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _voucherBL.GetAllRecords());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        /// <summary>
        /// lấy danh sách Tài sản và tổng số bản ghi
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns>danh sách tài sản và tổng số bản ghi</returns>
        /// NTD 22/8/2022

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PagingData<Voucher>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPaging(
            [FromQuery] String? keyword,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1)
        {
            try
            {

                var multipleResults = _voucherBL.GetPaging(keyword, pageSize, pageNumber);

                if (multipleResults != null)
                {
                    return StatusCode(StatusCodes.Status200OK, multipleResults);
                }

                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Validate);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }


        [HttpGet("listProperty/{voucherID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PagingData<Property>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPropertyInVoucher([FromRoute] Guid voucherID)
        {
            try
            {

                var multipleResults = _voucherBL.GetPropertyInVoucher(voucherID);

                if (multipleResults != null)
                {
                    return StatusCode(StatusCodes.Status200OK, multipleResults);
                }

                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Validate);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        /// <summary>
        /// lấy new code
        /// </summary>
        /// <returns></returns>
        /// NTD 22/8/2022
        [HttpGet("new-code")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(String))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult getNewVoucherCode()
        {
            try
            {
                var newVoucherCode = _voucherBL.GetNewVoucherCode();
                return StatusCode(StatusCodes.Status200OK, newVoucherCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }

        }

        /// <summary>
        /// lấy thông tin 1 voucher
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns>Thông tin tài sản</returns>
        /// NTD 22/8/2022

        [HttpGet("{voucherID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Property))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetVoucherByID([FromRoute] Guid voucherID)
        {

            try
            {

                var property = _voucherBL.GetVoucherByID(voucherID);

                if (property != null)
                {
                    return StatusCode(StatusCodes.Status200OK, property);
                }
                return StatusCode(StatusCodes.Status404NotFound);


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        /// <summary>
        /// hàm xóa 1 tài sản
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns>trả về ID tài sản vừa xóa</returns>
        /// NTD 22/8/2022
        [HttpDelete("{voucherID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteVoucherByID([FromRoute] Guid voucherID)
        {
            try
            {
                int numberAffectedRows = _voucherBL.DeleteVoucherByID(voucherID);

                //thực hiện trả kết quả về cho người dùng
                if (numberAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, voucherID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Validate);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        ///// <summary>
        ///// thêm mới 1 voucher
        ///// </summary>
        ///// <param name="voucher"></param>
        ///// <returns>trả về id của tài sản </returns>
        ///// NTD 22/8/2022
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult InsertVoucher([FromBody] NewVoucherData newVoucher)
        {


            try
            {
                var result = _voucherBL.InsertVoucher(newVoucher);
                return StatusCode(StatusCodes.Status201Created, result);
                
            }
            catch (Exception exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }


        }

        ///// <summary>
        ///// thêm mới 1 voucher
        ///// </summary>
        ///// <param name="voucher"></param>
        ///// <returns>trả về id của tài sản </returns>
        ///// NTD 22/8/2022
        [HttpPut("{voucherID}")]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateVoucher([FromRoute] Guid voucherID, [FromBody] NewVoucherData voucher)
        {


            try
            {
                var result = _voucherBL.UpdateVoucher(voucherID, voucher);
                return StatusCode(StatusCodes.Status201Created, result);

            }
            catch (Exception exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
            }


        }

        /// <summary>
        /// hàm xóa nhiều ghi tăng
        /// </summary>
        /// <param name="listVoucherID"></param>
        /// <returns>trả về ID tài sản vừa xóa</returns>
        /// NTD 22/8/2022
        [HttpPost("delete-multiple")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(int))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteMultipleVoucher([FromBody] List<Guid> listVoucherID)
        {
            try
            {
                int numberAffectedRows = _voucherBL.DeleteMultipleVoucher(listVoucherID);

                //thực hiện trả kết quả về cho người dùng
                if (numberAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, numberAffectedRows);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Validate);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }
        #endregion
    }
}
