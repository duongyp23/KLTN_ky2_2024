using KLTN.Common.Entity;

namespace KLTN.DataLayer
{
    public interface IOrderDL : IBaseDL<Order>
    {
        Task<List<Product>> GetProductInOrder(Guid id);
        Task<Order> GetWaitOrder(Guid userId);
        Task UpdateQuantityProduct(Guid? product_id);
    }
}