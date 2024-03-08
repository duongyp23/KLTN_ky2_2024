using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.HCSN2.DUONG.Common.Entity.DTO
{
    public class UpdateVoucherInfo
    {
        public Voucher VoucherData { get; set; }

        public List<Property> NewListProperty { get; set; }

    }
}
