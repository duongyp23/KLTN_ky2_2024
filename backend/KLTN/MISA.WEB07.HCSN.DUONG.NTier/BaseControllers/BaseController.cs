using KLTN.BussinesLayer;
using KLTN.Common.Entity.DTO;
using KLTN.Common.Entity;
using KLTN.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KLTN.NTier.Base
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>

        [HttpPost("GetAll")]
        public virtual async Task<IActionResult> GetAllRecords([FromBody] List<Filter>? filters)
        {
            try
            {
                List<T> result = await _baseBL.GetAllRecords(filters);
                return StatusCode(StatusCodes.Status200OK,result);
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

        [HttpPost("Paging")]
        public virtual async Task<IActionResult> GetPagingAsync(
            [FromBody] List<Filter>? filter, int pageSize = 20,int pageNumber = 1
            )
        {
            try
            {

                var multipleResults = await _baseBL.GetPaging(filter, pageSize, pageNumber);

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

        [HttpPost("Add")]

        public virtual async Task<IActionResult> Insert([FromBody]T entity)
        {
            try
            {
                Guid id = await _baseBL.Insert(entity);
                if(id != Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status200OK, id);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        [HttpPost("Update")]

        public virtual async Task<IActionResult> Update([FromBody] T entity)
        {
            try
            {
                bool result = await _baseBL.Update(entity);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }


        [HttpDelete("Delete/{id}")]

        public virtual async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                bool result = await _baseBL.Delete(id);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        [HttpGet("{id}")]

        public virtual async Task<IActionResult> GetDataById(Guid id)
        {
            try
            {
                T record = await _baseBL.GetDataById(id);
                return StatusCode(StatusCodes.Status200OK, record);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        #endregion
    }
}