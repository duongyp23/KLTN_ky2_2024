using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.HCSN2.DUONG.BL.PropertyBL;
using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.Common.Entity.DTO;
using Swashbuckle.AspNetCore.Annotations;
using MISA.WEB07.HCSN2.DUONG.NTier.Base;
using MySqlConnector;
using Microsoft.AspNetCore.Authorization;
using MISA.WEB07.HCSN2.DUONG.Common.Enums;

namespace MISA.HCSN2.API.NTier
{
    
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertiesController : Controller
    {
        #region Field

        private IPropertyBL _propertyBL;

        #endregion

        #region Constructor

        public PropertiesController(IPropertyBL propertyBL)
        {
            _propertyBL = propertyBL;
        }

        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả danh sách tài sản
        /// </summary>
        /// <returns></returns>
        [HttpGet("all-record")]
        public IActionResult GetAllProperties()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _propertyBL.GetAllRecords());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        
        /// <summary>
        /// hàm xóa 1 tài sản
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns>trả về ID tài sản vừa xóa</returns>
        /// NTD 22/8/2022
        [HttpDelete("{propertyID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePropertyByID([FromRoute] Guid propertyID)
        {
            try
            {
                int numberAffectedRows = _propertyBL.DeletePropertyByID(propertyID);

                //thực hiện trả kết quả về cho người dùng
                if (numberAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, propertyID);
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
        /// <summary>
        /// lấy new code
        /// </summary>
        /// <returns></returns>
        /// NTD 22/8/2022
        [HttpGet("new-code")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(String))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult getNewPropertyCode()
        {
            try
            {
                var newPropertyCode = _propertyBL.GetNewPropertyCode();
                return StatusCode(StatusCodes.Status200OK, newPropertyCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }

        }
        /// <summary>
        /// lấy thông tin 1 Tài sản
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns>Thông tin tài sản</returns>
        /// NTD 22/8/2022
        
        [HttpGet("{propertyID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Property))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPropertyByID([FromRoute] Guid propertyID)
        {

            try
            {

                var property = _propertyBL.GetPropertyByID(propertyID);

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
        /// lấy danh sách Tài sản và tổng số bản ghi
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentID"></param>
        /// <param name="propertyTypeID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="isActive"></param>
        /// <returns>danh sách tài sản và tổng số bản ghi</returns>
        /// NTD 22/8/2022

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PagingData<Property>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPaging(
            [FromQuery] String? keyword,
            [FromQuery] Guid? departmentID,
            [FromQuery] Guid? propertyTypeID,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1
            )
        {
            try
            {

                var multipleResults = _propertyBL.GetPaging(keyword, departmentID, propertyTypeID, pageSize, pageNumber);

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

        [HttpPost("not-active")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PagingData<Property>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPagingNotActive(
            [FromBody] List<Guid>? listPropertyID,
            [FromQuery] String? keyword,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1
            
            )
        {
            try
            {

                var multipleResults = _propertyBL.GetPagingNotActive(keyword,pageSize, pageNumber, listPropertyID);

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
        /// thêm mới 1 tài sản
        /// </summary>
        /// <param name="property"></param>
        /// <returns>trả về id của tài sản </returns>
        /// NTD 22/8/2022
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult InsertProperty([FromBody] Property property)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _propertyBL.InsertProperty(property);

                    if (result > 0)
                    {
                        return StatusCode(StatusCodes.Status201Created, result);
                    }
                    return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Validate);
                }
                catch (Exception exception)
                {
                    
                    return StatusCode(StatusCodes.Status400BadRequest, exception.Message);
                }
            }
            else
            {
                return View(property);
            } 
            
        }

        /// <summary>
        /// sửa 1 tài sản
        /// </summary>
        /// <param name="property"></param>
        /// <returns>trả về id của tài sản </returns>
        /// NTD 22/8/2022
        [HttpPut("{propertyID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateEmployee([FromRoute] Guid propertyID, [FromBody] Property property)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    var numberOfAffectedRows = _propertyBL.UpdateProperty(propertyID, property);

                    if (numberOfAffectedRows > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, propertyID);
                    }
                    else
                    {
                        // Lỗi không UPDATE được 
                        return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Validate);
                    }
                }
                catch (Exception exception)
                {
                    // Lỗi ex chung
                    Console.WriteLine(exception.Message);
                    return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
                }
            }
            else
            {
                return View(property);
            } 
            
        }

        /// <summary>
        /// hàm xóa nhiều tài sản
        /// </summary>
        /// <param name="ListPropertyID"></param>
        /// <returns>trả về ID tài sản vừa xóa</returns>
        /// NTD 22/8/2022
        [HttpPost("delete-multiple")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(int))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteMultipleProperty([FromBody]List<Guid> ListPropertyID)
        {
            try
            {
                int numberAffectedRows = _propertyBL.DeleteMultipleProperty(ListPropertyID);

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

        /// <summary>
        /// Thêm mới nhiều tài sản 
        /// </summary>
        /// <param name="ListProperty"></param>
        /// <returns></returns>
        [HttpPost("import-multiple")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(int))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult ImportMultipleProperty([FromBody] List<Property> ListProperty)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int numberAffectedRows = _propertyBL.ImportMultipleProperty(ListProperty);

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
                    
                    return StatusCode(StatusCodes.Status400BadRequest, e.Message);
                }
            }
            else
            {
                return View(ListProperty);
            }
        }

        #endregion
    }
}
