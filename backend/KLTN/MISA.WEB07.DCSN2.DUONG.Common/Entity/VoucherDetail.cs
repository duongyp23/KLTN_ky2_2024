using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN.Common.Entity
{
    /// <summary>
    /// Bảng thông tin chi tiết của ghi tăng
    /// </summary>
    public class VoucherDetail
    {

        /// <summary>
        /// ID voucher
        /// </summary>
        public Guid VoucherID { get; set; }


        /// <summary>
        /// ID tài sản
        /// </summary>
        public Guid PropertyID { get; set; }

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
