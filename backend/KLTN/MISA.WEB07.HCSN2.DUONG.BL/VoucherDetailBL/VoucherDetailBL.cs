using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.DL;
using MISA.WEB07.HCSN2.DUONG.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.HCSN2.DUONG.BL
{
    public class VoucherDetailBL : BaseBL<VoucherDetail>, IVoucherDetailBL
    {
        #region Field

        private IVoucherDetailDL _voucherDetailDL;

        #endregion

        #region Constructor

        public VoucherDetailBL(IVoucherDetailDL voucherDetailDL) : base(voucherDetailDL)
        {
            _voucherDetailDL = voucherDetailDL;
        }

        

        #endregion
        #region Mewthod
        /// <summary>
        /// Thêm mới 1 voucher detail
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="propertyID"></param>
        /// <returns></returns>
        public int InsertVoucherDetail(Guid voucherID, Guid propertyID)
        {
            return _voucherDetailDL.InsertVoucherDetail(voucherID, propertyID);
        }
        /// <summary>
        /// Xóa voucher detail
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        public int DeleteVoucherDetail(Guid voucherID)
        {
            return _voucherDetailDL.DeleteVoucherDetail(voucherID);
        }
        #endregion
    }
}