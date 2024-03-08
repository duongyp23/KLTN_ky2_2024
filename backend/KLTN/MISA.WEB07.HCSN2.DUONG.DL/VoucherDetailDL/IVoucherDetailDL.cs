using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.HCSN2.DUONG.DL
{
    public interface IVoucherDetailDL : IBaseDL<VoucherDetail>
    {
        /// <summary>
        /// Thêm mới voucher detail
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="propertyID"></param>
        /// <returns></returns>
        public int InsertVoucherDetail(Guid voucherID, Guid propertyID);
        /// <summary>
        /// Xóa voucherdetail
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        public int DeleteVoucherDetail(Guid voucherID);
    }
}