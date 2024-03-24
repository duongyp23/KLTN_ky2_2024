using KLTN.BussinesLayer;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using KLTN.Common.Enums;
using KLTN.NTier.Base;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KLTN.NTier.Controllers
{
    public class CategorysController : BasesController<Category>
    {
        #region Field

        private ICategoryBL _categoryBL;

        #endregion

        #region Constructor

        public CategorysController(ICategoryBL categoryBL) : base(categoryBL)
        {
            _categoryBL = categoryBL;
        }

        #endregion

        #region Method
        /// <summary>
        /// API Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>

        [HttpGet("GetCategoryOfProduct")]
        public virtual async Task<IActionResult> GetCategoryOfProduct(Guid productId)
        {
            try
            {
                List<Category> result = await _categoryBL.GetCategoryOfProduct(productId);
                return StatusCode(StatusCodes.Status200OK, result);
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
