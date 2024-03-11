using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN.Common.Command
{
    public class VoucherDetailCommand
    {
        public static String deleteVoucherDetail = "DELETE FROM voucherdetail WHERE VoucherID = @VoucherID";
    }
}
