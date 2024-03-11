using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN.Common.Entity.DTO
{
    public class NewVoucherData
    {
        /// <summary>
        /// Thông tin của voucher
        /// </summary>
        public Voucher VoucherData { get; set; }

        /// <summary>
        /// Thông tin danh sách tài sản kèm theo
        /// </summary>

        public List<Property> ListProperty { get; set; }


    }
}
