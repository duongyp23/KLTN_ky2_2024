using KLTN.Common.Entity;
using KLTN.DataLayer;

namespace KLTN.BussinesLayer
{
    public class ProductBL : BaseBL<Product>, IProductBL
    {
        #region Field

        private IProductDL _productDL;

        #endregion

        #region Constructor

        public ProductBL(IProductDL productDL) : base(productDL)
        {
            _productDL = productDL;
        }

        #endregion
    }
}