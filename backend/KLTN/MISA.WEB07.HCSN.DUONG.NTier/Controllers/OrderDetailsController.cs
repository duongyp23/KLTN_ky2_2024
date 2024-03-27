using KLTN.BussinesLayer;
using KLTN.Common.Entity;
using KLTN.NTier.Base;

namespace KLTN.NTier.Controllers
{
    public class OrderDetailsController : BasesController<OrderDetail>
    {
        #region Field

        private IOrderDetailBL _orderDetailBL;

        #endregion

        #region Constructor

        public OrderDetailsController(IOrderDetailBL orderDetailBL) : base(orderDetailBL)
        {
            _orderDetailBL = orderDetailBL;
        }

        #endregion

        #region Method
        #endregion
    }
}
