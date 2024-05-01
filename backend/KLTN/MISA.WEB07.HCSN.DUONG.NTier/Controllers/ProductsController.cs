using KLTN.BussinesLayer;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using KLTN.Common.Enums;
using KLTN.NTier.Base;
using Microsoft.AspNetCore.Mvc;

namespace KLTN.NTier.Controllers
{
    public class ProductsController : BasesController<Product>
    {
        #region Field

        private IProductBL _productBL;

        #endregion

        #region Constructor

        public ProductsController(IProductBL productBL) : base(productBL)
        {
            _productBL = productBL;
        }

        #endregion

        #region Method
        [HttpPost("AddProduct")]

        public async Task<IActionResult> Insert([FromBody] ProductData data)
        {
            try
            {
                Guid id = await _productBL.InsertProduct(data);
                if (id != Guid.Empty)
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

        [HttpPost("UpdateProduct")]

        public async Task<IActionResult> Update([FromBody] ProductData data)
        {
            try
            {
                bool result = await _productBL.UpdateProduct(data);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        [HttpPost("AddProductToCart")]

        public async Task<IActionResult> AddProductToCart(Guid productId, Guid userId, string optionCode, int quantity)
        {
            try
            {
                bool result = await _productBL.AddProductToCart(productId, userId, optionCode, quantity);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }
        #endregion
    }
}
