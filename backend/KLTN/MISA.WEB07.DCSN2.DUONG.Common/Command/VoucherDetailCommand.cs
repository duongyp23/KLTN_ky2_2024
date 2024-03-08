using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.HCSN2.DUONG.Common.Command
{
    public class VoucherDetailCommand
    {
        public static String deleteVoucherDetail = "DELETE FROM voucherdetail WHERE VoucherID = @VoucherID";
    }
}
