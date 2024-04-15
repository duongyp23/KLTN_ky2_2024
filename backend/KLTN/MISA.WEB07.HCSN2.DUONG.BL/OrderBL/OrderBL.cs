using System.Web.WebPages.Html;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using KLTN.DataLayer;
using Newtonsoft.Json;
using ZaloPay.Helper;
using ZaloPay.Helper.Crypto;

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
        public string key1 = "sdngKKJmqEMzvh5QQcdD2A9XBSKUNaYn";

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

        public async Task<object> Payment(Guid id)
        {
            Order order = await _orderDL.GetDataById(id);
            string appid = "2554";
            string key2 = "trMrHtvjo6myautxDUiAcYsVtaeQ8nhf";
            string createOrderUrl = "https://sandbox.zalopay.com.vn/v001/tpe/createorder";

            var transid = order.order_id.ToString();
            var embeddata = new { merchantinfo = "embeddata123", redirecturl = "http://localhost:8080/userinfo" };
            var param = new Dictionary<string, string>();

            param.Add("appid", appid);
            param.Add("appuser", "demo");
            param.Add("apptime", Utils.GetTimeStamp().ToString());
            param.Add("amount", order.total_order_deposit.ToString());
            param.Add("apptransid", DateTime.Now.ToString("yyMMdd") + "_" + transid); // mã giao dich có định dạng yyMMdd_xxxx
            param.Add("embeddata", JsonConvert.SerializeObject(embeddata));
            param.Add("item","");
            param.Add("description", "Thanh toán đơn hàng");
            param.Add("bankcode", "");

            var data = appid + "|" + param["apptransid"] + "|" + param["appuser"] + "|" + param["amount"] + "|"
                + param["apptime"] + "|" + param["embeddata"] + "|" + param["item"];
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

            var result = await HttpHelper.PostFormAsync(createOrderUrl, param);

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