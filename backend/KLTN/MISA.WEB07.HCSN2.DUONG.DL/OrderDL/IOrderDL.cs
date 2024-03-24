using KLTN.Common.Entity;

namespace KLTN.DataLayer
{
    public interface IOrderDL : IBaseDL<Order>
    {
        Task<Guid> GetWaitOrder(Guid userId);
    }
}