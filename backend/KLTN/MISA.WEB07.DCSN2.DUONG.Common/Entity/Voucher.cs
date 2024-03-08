using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.HCSN2.DUONG.Common.Entity
{
    /// <summary>
    /// Bảng ghi tăng
    /// </summary>
    public class Voucher
    {
        /// <summary>
        /// ID ghi tăng
        /// </summary>
        public Guid VoucherID { get; set; }

        /// <summary>
        /// Mã ghi tăng
        /// </summary>
        [Required(ErrorMessage = "Mã tài sản không tồn tại"), StringLength(20, ErrorMessage = "Mã tài sản tối đa 20 ký tự")]
        public string VoucherCode { get; set; }

        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        [Required(ErrorMessage = "Ngày ghi tăng không tồn tại")]
        public DateTime IncrementDate { get; set; }

        /// <summary>
        /// Ngày abwts đầu sử dụng
        /// </summary>
        [Required(ErrorMessage = "Ngày bắt đầu sử dụng không tồn tại")]
        public DateTime StartUsingDate { get; set; }

        /// <summary>
        /// Tổng tài sản
        /// </summary>
        [Required(ErrorMessage = "Tổng tài sản không tồn tại")]
        [Range(minimum: 0, maximum: 999999999999999999.9999, ErrorMessage = "Giá trị hao mòn phải nhỏ hơn 100 triệu tỷ")]
        public double TotalPrice { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        [StringLength(100)]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        [StringLength(100)]
        public string? ModifiedBy { get; set; }
        
    }
}
