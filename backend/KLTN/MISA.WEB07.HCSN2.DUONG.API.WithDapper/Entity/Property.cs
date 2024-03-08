namespace MISA.WEB07.HCSN2.DUONG.API.WithDapper.Entity
{
    public class Property
    {
        /// <summary>
        /// ID tài sản
        /// </summary>
        public Guid PropertyID { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string PropertyCode { get; set; }

        /// <summary>
        /// ID loại tài sản
        /// </summary>
        public Guid? PropertyTypeID { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string PropertyTypeName { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>      
        public string PropertyTypeCode { get; set; }

        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid? DepartmentID { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>

        public int MarkedPrice { get; set; }
        /// <summary>
        /// Tỉ lệ hao mòn
        /// </summary>
        public decimal AttritionRate { get; set; }
        /// <summary>
        /// Giá trị hao mòn
        /// </summary>
        public int AttritionValue  { get; set; }
        /// <summary>
        /// số năm sử dụng
        /// </summary>
        public int UsedYear { get; set; }
        /// <summary>
        /// năm theo dõi
        /// </summary>
        public int TrackingYear { get; set; }
        /// <summary>
        /// số lượng
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Ngày mua
        /// </summary>

        public DateTime PurchasingDate { get; set; }
        /// <summary>
        /// ngày bắt đầu sử dụng
        /// </summary>
        public DateTime StartUsingDate { get; set; }
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
