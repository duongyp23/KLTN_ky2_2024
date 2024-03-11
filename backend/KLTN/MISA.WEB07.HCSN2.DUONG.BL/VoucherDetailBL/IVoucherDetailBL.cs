using KLTN.Common.Entity;
using KLTN.BussinesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN.BussinesLayer
{
    public interface IVoucherDetailBL : IBaseBL<VoucherDetail>
    {
        /// <summary>
        /// Thêm mới voucherdetail
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