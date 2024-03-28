using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using KLTN.DataLayer;

namespace KLTN.BussinesLayer
{
    public class OrderBL : BaseBL<Order>, IOrderBL
    {
        #region Field

        private IOrderDL _orderDL;
        private IOrderDetailDL _orderDetailDL;

        #endregion

        #region Constructor

        public OrderBL(IOrderDL orderDL, IOrderDetailDL orderDetailDL) : base(orderDL)
        {
            _orderDL = orderDL;
            _orderDetailDL = orderDetailDL;
        }

        public async Task<OrderData> GetOrderData(Guid id)
        {
            Order order = await _orderDL.GetDataById(id);
            if (order != null)
            {
                List<Filter> filters = new List<Filter>()
                {
                    new Filter()
                    {
                        columnName = "order_id",
                        filterValue = order.order_id,
                        operatorValue = "="
                    }
                };
                List<OrderDetail> orderDetails = await _orderDetailDL.GetDataByField(filters);
                return new OrderData()
                {
                    order = order,
                    orderDetails = orderDetails
                };
            }
            else
            {
                throw new Exception("Không tồn tại đơn hàng chờ");
            }
        }

        public async Task<List<Order>> GetOrderOfUser(Guid id)
        {
            List<Filter> filters = new List<Filter>()
                {
                    new Filter()
                    {
                        columnName = "user_id",
                        filterValue = id,
                        operatorValue = "="
                    },
                    new Filter()
                    {
                        columnName = nameof(Order.status),
                        filterValue =  1,
                        operatorValue = "<>"
                    }
                };
            List<Order> result = await _orderDL.GetDataByField(filters);
            return result;
        }

        #endregion
    }
}