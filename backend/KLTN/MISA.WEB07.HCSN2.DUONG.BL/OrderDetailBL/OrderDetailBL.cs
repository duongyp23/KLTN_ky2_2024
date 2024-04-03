using KLTN.Common.Entity;
using KLTN.DataLayer;

namespace KLTN.BussinesLayer
{
    public class OrderDetailBL : BaseBL<OrderDetail>, IOrderDetailBL
    {
        #region Field

        private IOrderDetailDL _orderDetailDL;
        private IProductDL _productDL;

        #endregion

        #region Constructor

        public OrderDetailBL(IOrderDetailDL orderDetailDL, IProductDL productDL) : base(orderDetailDL)
        {
            _orderDetailDL = orderDetailDL;
            _productDL = productDL;
        }


        #endregion

        #region Method
        #endregion
    }
}