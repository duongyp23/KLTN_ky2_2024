using KLTN.Common.Entity.DTO;

namespace KLTN.BussinesLayer
{
    public interface IBaseBL<T>
    {
         Task<bool> Delete(Guid id);

        /// <summary>
        /// lấy tất cả bản ghi
        /// Author: TTDUC 24/08/2022
        /// </summary>
        /// <returns>tất cả bản ghi của 1 bảng</returns>
        Task<List<T>> GetAllRecords(List<Filter>? filters);
        Task<T> GetDataById(Guid id);
        Task<PagingData<T>> GetPaging(List<Filter>? filter, int pageSize, int pageNumber);
         Task<Guid> Insert(T entity);
         Task<bool> Update(T entity);
    }
}
