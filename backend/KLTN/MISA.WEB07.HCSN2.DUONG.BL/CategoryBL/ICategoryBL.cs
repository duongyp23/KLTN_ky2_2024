using KLTN.Common.Entity;

namespace KLTN.BussinesLayer
{
    public interface ICategoryBL : IBaseBL<Category>
    {
        Task<List<Category>> GetCategoryOfProduct(Guid productId);
    }
}