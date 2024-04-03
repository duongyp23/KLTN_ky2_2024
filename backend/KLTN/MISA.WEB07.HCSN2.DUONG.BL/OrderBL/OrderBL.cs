using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using KLTN.DataLayer;
using static Dapper.SqlMapper;

namespace KLTN.BussinesLayer
{
    public class OrderBL : BaseBL<Order>, IOrderBL
    {
        #region Field

        private IOrderDL _orderDL;
        private IOrderDetailDL _orderDetailDL;
        private IProductDL _productDL;

        #endregion

        #region Constructor

        public OrderBL(IOrderDL orderDL, IOrderDetailDL orderDetailDL, IProductDL productDL) : base(orderDL)
        {
            _orderDL = orderDL;
            _orderDetailDL = orderDetailDL;
            _productDL = productDL;
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

        public async Task<bool> UpdateOrderData(OrderData orderData)
        {
            await _orderDL.Update(orderData.order);
            int status = 0;
            string statusName = "";
            switch (orderData.order.status)
            {
                case 1:
                    status = 1;
                    statusName = "Chưa cho thuê";
                    break;
                case 2:
                case 3:
                    status = 2;
                    statusName = "Đã cho thuê";
                    break;
                case 5:
                    status = 1;
                    statusName = "Chưa cho thuê";
                    break;
                default:
                    break;
            }
            foreach (OrderDetail detail in orderData.orderDetails)
            {
                await _orderDetailDL.Update(detail);
                
                if(status != 0)
                {
                    Product product = new Product()
                    {
                        product_id = detail.product_id,
                        status = detail.order_type == 0 ? status : 3,
                        status_name = detail.order_type == 0 ? statusName : "Đã bán",
                    };
                    await _productDL.Update(product);
                }
            }
            return true;
        }

        #endregion
    }
}