using System.ComponentModel.DataAnnotations;

namespace MISA.WEB07.HCSN2.DUONG.Common.Entity
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
        [Required(ErrorMessage ="Tên tài sản không tồn tại")]
        [StringLength(255, ErrorMessage = "Têm tài sản có tối đa 255 kỹ tự ")]
        public string PropertyName { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        [Required(ErrorMessage = "Mã tài sản không tồn tại"),StringLength(20, ErrorMessage ="Mã tài sản tối đa 20 ký tự")]
        public string PropertyCode { get; set; }

        /// <summary>
        /// ID loại tài sản
        /// </summary>
        [Required(ErrorMessage = "ID loại tài sản không tồn tại")]
        public Guid? PropertyTypeID { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        [Required(ErrorMessage ="Tên loại tài sản không tồn tại")]
        [StringLength(255, ErrorMessage ="Tên loại tài sản tối đa 255 ký tự")]
        public string PropertyTypeName { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>      
        [Required(ErrorMessage = "Mã loại tài sản không tồn tại")]
        [StringLength(20, ErrorMessage ="Mã loại tài sản tối đa 20 ký tự")]
        public string PropertyTypeCode { get; set; }

        /// <summary>
        /// ID phòng ban
        /// </summary>
        [Required(ErrorMessage ="ID phòng ban không tồn tại")]
        public Guid? DepartmentID { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Required(ErrorMessage ="Tên phòng ban không tồn tại")]
        [StringLength(255, ErrorMessage ="Tên phòng ban tái đa 255 ký tự")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [Required(ErrorMessage = "Mã phòng ban không tồn tại")]
        [StringLength(20, ErrorMessage ="Mã phòng ban tối đa 20 ký tự")]
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>
        [Required(ErrorMessage ="Nguyên giá không tồn tại")]
        [Range(minimum: 0.0001,maximum: 999999999999999999.9999, ErrorMessage ="Giá tài sản nhỏ hơn 100 triệu tỷ")]
        public double MarkedPrice { get; set; }
        /// <summary>
        /// Tỉ lệ hao mòn
        /// </summary>
        [Required(ErrorMessage ="Tỉ lệ hao mòn không tồn tại")]
        [Range(minimum:0.0001,maximum:1, ErrorMessage ="tỉ lệ hao mòn nhỏ hơn 1")]
        public decimal AttritionRate { get; set; }
        /// <summary>
        /// Giá trị hao mòn
        /// </summary>
        [Required(ErrorMessage ="Giá trị hao mòn không tồn tại")]
        [Range(minimum:0,maximum: 999999999999999999.9999, ErrorMessage ="Giá trị hao mòn phải nhỏ hơn 100 triệu tỷ")]
        public double AttritionValue  { get; set; }
        /// <summary>
        /// số năm sử dụng
        /// </summary>
        [Required(ErrorMessage = "Số năm sử dụng không tồn tại")]
        [Range(minimum:1, maximum:100, ErrorMessage ="Số năm sử dụng tối đa là 100")]
        public int UsedYear { get; set; }
        /// <summary>
        /// năm theo dõi
        /// </summary>
        [Required(ErrorMessage ="Năm theo dõi không tồn tại")]
        [Range(minimum:2000,maximum: 2022, ErrorMessage ="Năm theo dõi phải nhỏ hơn hiện tại")]
        public int TrackingYear { get; set; }
        /// <summary>
        /// số lượng
        /// </summary>
        [Required(ErrorMessage ="Số lượng không tồn tại")]
        public int Quantity { get; set; }
        /// <summary>
        /// Ngày mua
        /// </summary>
        [Required(ErrorMessage ="Ngày mua không tồn tại")]
        public DateTime PurchasingDate { get; set; }
        /// <summary>
        /// ngày bắt đầu sử dụng
        /// </summary>
        [Required(ErrorMessage ="Ngày bắt đầu sử dụng không tồn tại")]
        public DateTime StartUsingDate { get; set; }

        /// <summary>
        /// trạng thái
        /// </summary>
        [Required]
        public int IsActive { get; set; }

        /// <summary>
        /// nguồn tài sản
        /// </summary>
        [Required]
        public string Budget { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        [StringLength(100)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        [StringLength(100)]
        public string ModifiedBy { get; set; }
    }
}
