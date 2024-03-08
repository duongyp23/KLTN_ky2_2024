namespace MISA.WEB07.HCSN2.DUONG.API.WithDapper.Entity
{
    public class PropertyType
    {
        /// <summary>
        /// ID loại tài sản
        /// </summary>
        public Guid PropertyTypeID { get; set; }

        /// <summary>
        /// Mã Loại tài sản
        /// </summary>
        public string PropertyTypeCode { get; set; }

        /// <summary>
        /// Tên Loại tìa sản
        /// </summary>
        public string PropertyTypeName { get; set; }

        /// <summary>
        /// Mã tỉ lệ hao mòn mặc định
        /// </summary>
        public decimal AttritionRateDefault { get; set; }

        /// <summary>
        /// Số năm sử dụng mặc định
        /// </summary>

        public int UsedYearDefault { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
