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
        private IStockDL _stockDL;

        #endregion

        #region Constructor
        public string key1 = "sdngKKJmqEMzvh5QQcdD2A9XBSKUNaYn";

        public OrderBL(IOrderDL orderDL, IOrderDetailDL orderDetailDL, IProductDL productDL, IStockDL stockDL) : base(orderDL)
        {
            _orderDL = orderDL;
            _orderDetailDL = orderDetailDL;
            _productDL = productDL;
            _stockDL = stockDL;
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
                        filterValue = "'"+order.order_id+"'",
                        operatorValue = "="
                    }
                };
                List<OrderDetail> orderDetails = await _orderDetailDL.GetDataByField(filters);
                List<Product> products = await _orderDL.GetProductInOrder(id);
                return new OrderData()
                {
                    order = order,
                    orderDetails = orderDetails,
                    products = products
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
                        filterValue = "'"+id+"'",
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
            foreach (OrderDetail detail in orderData.orderDetails)
            {
                await _orderDetailDL.Update(detail);
                List<Filter> filters = new List<Filter>()
                {
                    new Filter()
                    {
                        columnName = "option_code",
                        filterValue ='"'+detail.option_code+'"',
                        operatorValue = "="
                    },
                    new Filter()
                    {
                        columnName = "product_id",
                        filterValue =  '"' + detail.product_id.ToString() + '"',
                        operatorValue = "="
                    }
                };
                List<Stock> stocks = await _stockDL.GetDataByField(filters);
                if(stocks != null && stocks.Count == 1)
                {
                    Stock stock = stocks[0];
                    if (orderData.order.status == 3)
                    {
                        stock.quantity_rental = stock.quantity_rental + detail.quantity;
                        stock.quantity_remain = stock.quantity_remain - detail.quantity;
                    } else if(orderData.order.status == 5)
                    {
                        stock.quantity_rental = stock.quantity_rental - detail.quantity;
                        stock.quantity_remain = stock.quantity_remain + detail.quantity; 
                    }
                    await _stockDL.Update(stock);
                }
            }
            return true;
        }

        public async Task<bool> AddStockOrder(OrderData orderData)
        {
            Guid id = await _orderDL.Insert(orderData.order);
            foreach (OrderDetail item in orderData.orderDetails)
            {
                item.order_id = id;
                await _orderDetailDL.Insert(item);
                List<Filter> filters = new List<Filter>()
                {
                    new Filter()
                    {
                        columnName = "option_code",
                        filterValue ='"'+item.option_code+'"',
                        operatorValue = "="
                    },
                    new Filter()
                    {
                        columnName = "product_id",
                        filterValue =  '"' + item.product_id.ToString() + '"',
                        operatorValue = "="
                    }
                };
                List<Stock> stocks = await _stockDL.GetDataByField(filters);
                if(stocks != null && stocks.Count == 1)
                {
                    Stock stock = stocks[0];
                    if(orderData.order.order_type == 2)
                    {
                        stock.total_quantity = stock.total_quantity + item.quantity;
                    } else if(orderData.order.order_type == 3)
                    {
                        stock.total_quantity = stock.total_quantity - item.quantity;
                        stock.quantity_sold = stock.quantity_sold + item.quantity;
                    }
                    await _stockDL.Update(stock);
                } else
                {
                    Stock stock = new Stock()
                    {
                        stock_id = Guid.NewGuid(),
                        product_id = item.product_id,
                        total_quantity = item.quantity,
                        quantity_remain = item.quantity,
                        quantity_rental = 0,
                        quantity_sold = 0,
                        option_code = item.option_code
                    };
                    await _stockDL.Insert(stock);
                }
            }
            return true;
        }

        #endregion
    }
}