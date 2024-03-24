using KLTN.Common.Entity;

namespace KLTN.DataLayer
{
    public interface IProductCategoryDL : IBaseDL<ProductCategory>
    {
        Task RemoveOldCategory(Guid? product_id);
    }
}