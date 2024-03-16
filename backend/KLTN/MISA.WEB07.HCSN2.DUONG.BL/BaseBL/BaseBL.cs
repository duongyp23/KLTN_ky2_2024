using KLTN.Common.Entity.DTO;
using KLTN.DataLayer;
using static Dapper.SqlMapper;

namespace KLTN.BussinesLayer
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion


        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion


        #region Method



        /// <summary>
        /// lấy tất cả bản ghi
        /// </summary>
        /// <returns>tất cả bản ghi của 1 bảng</returns>
        public virtual IEnumerable<dynamic> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        async Task<Guid> IBaseBL<T>.Insert(T entity)
        {
            return await _baseDL.Insert(entity);
        }

        async Task<PagingData<T>> IBaseBL<T>.GetPaging(List<Filter>? filter, int pageSize, int pageNumber)
        {
            return await _baseDL.GetPaging(filter, pageSize, pageNumber);
        }

        async Task<bool> IBaseBL<T>.Update(T entity)
        {
            return await _baseDL.Update(entity);
        }

        async Task<bool> IBaseBL<T>.Delete(Guid id)
        {
            return await _baseDL.Delete(id);
        }

        #endregion
    }
}
