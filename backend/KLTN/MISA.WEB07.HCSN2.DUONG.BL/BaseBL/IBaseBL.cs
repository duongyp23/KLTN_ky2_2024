using KLTN.Common.Entity.DTO;

namespace KLTN.BussinesLayer
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// lấy tất cả bản ghi
        /// Author: TTDUC 24/08/2022
        /// </summary>
        /// <returns>tất cả bản ghi của 1 bảng</returns>
        public IEnumerable<dynamic> GetAllRecords();
        Task<PagingData<T>> GetPaging(List<Filter>? filter, int pageSize, int pageNumber);
        Task<Guid> Insert(T entity);
    }
}
