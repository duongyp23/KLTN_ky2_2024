using KLTN.Common.Entity;
using KLTN.DataLayer;

namespace KLTN.BussinesLayer
{
    public class OrderDetailBL : BaseBL<OrderDetail>, IOrderDetailBL
    {
        #region Field

        private IOrderDetailDL _orderDetailDL;

        #endregion

        #region Constructor

        public OrderDetailBL(IOrderDetailDL orderDetailDL) : base(orderDetailDL)
        {
            _orderDetailDL = orderDetailDL;
        }


        #endregion
    }
}