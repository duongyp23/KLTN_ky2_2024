using KLTN.Common.Entity;

namespace KLTN.DataLayer
{
    public interface ICategoryDL : IBaseDL<Category>
    {
        Task<List<Category>> GetCategoryOfProduct(Guid productId);
    }
}