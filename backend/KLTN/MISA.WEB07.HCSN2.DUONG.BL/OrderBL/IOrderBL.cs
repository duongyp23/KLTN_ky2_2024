using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;

namespace KLTN.BussinesLayer
{
    public interface IOrderBL : IBaseBL<Order>
    {
        Task<bool> AddStockOrder(OrderData orderData);
        Task<OrderData> GetOrderData(Guid id);
        Task<List<Order>> GetOrderOfUser(Guid id);
        Task<object> Payment(Guid id);
        Task<bool> UpdateOrderData(OrderData orderData);
    }
}