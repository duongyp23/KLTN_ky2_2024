using KLTN.Common.Entity.DTO;

namespace KLTN.DataLayer
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// lấy tất cả bản ghi
        /// </summary>
        /// <returns>tất cả bản ghi của 1 bảng</returns>
        public IEnumerable<dynamic> GetAllRecords();
        /// <summary>
        /// lấy danh sách phân trang
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<PagingData<T>> GetPaging(List<Filter>? filter, int pageSize, int pageNumber);
        Task<Guid> Insert(T entity);
    }
}
