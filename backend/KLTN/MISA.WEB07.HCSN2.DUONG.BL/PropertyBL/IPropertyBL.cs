using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.Common.Entity.DTO;

namespace MISA.WEB07.HCSN2.DUONG.BL.PropertyBL
{
    public interface IPropertyBL : IBaseBL<Property>
    {
        /// <summary>
        /// hàm thêm mới 1 tài sản
        /// </summary>
        /// <param name="property"></param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// NTD 22/8/2022

        public int InsertProperty( Property property);

        /// <summary>
        /// Sửa 1 Tài sản
        /// </summary>
        /// <param name="propertyID"></param>
        /// <param name="property"></param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// NTD 22/8/2022
        public int UpdateProperty(Guid propertyID, Property property);



        /// <summary>
        /// hàm xóa nhiều tài sản
        /// </summary>
        /// <param name="ListPropertyID"></param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// NTD 22/8/2022
        public int DeleteMultipleProperty( List<Guid> ListPropertyID);

        /// <summary>
        /// hàm xóa nhiều tài sản
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// NTD 22/8/2022
        public int DeletePropertyByID(Guid propertyID);


        /// <summary>
        /// lấy thông tin 1 Tài sản
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns>Thông tin tài sản</returns>
        /// NTD 22/8/2022
        public Object GetPropertyByID(Guid propertyID);

        /// <summary>
        /// lấy danh sách Tài sản và tổng số bản ghi
        /// </summary>
        /// <param name="keyword">điều kiện lọc</param>
        /// <param name="departmentID">Id Phòng ban</param>
        /// <param name="propertyTypeID">Id loại tài sản</param>
        /// <param name="pageSize">số item trong 1 trang</param>
        /// <param name="pageNumber">số trang muốn lấy </param>
        /// <returns>danh sách tài sản và tổng số bản ghi</returns>
        /// NTD 22/8/2022
        public PagingData<Property> GetPaging(
             String? keyword,
             Guid? departmentID,
             Guid? propertyTypeID,
             int pageSize,
             int pageNumber
             
            );

        /// <summary>
        /// lấy danh sách tài sản chưa active
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="listPropertyID"></param>
        /// <returns></returns>
        public PagingData<Property> GetPagingNotActive(
             String? keyword,
             int pageSize,
             int pageNumber,
             List<Guid>? listPropertyID
            );

        /// <summary>
        /// lấy new code
        /// </summary>
        /// <returns></returns>
        /// NTD 22/8/2022
        public String GetNewPropertyCode();

        /// <summary>
        /// Thêm mới nhiều tài sản
        /// </summary>
        /// <param name="ListProperty"></param>
        /// <returns></returns>
        public int ImportMultipleProperty( List<Property> ListProperty );

        /// <summary>
        /// Kiểm tra tài sản thỏa màn các điều kiện hay không
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public void CheckProperty(Property property);
    }
}
