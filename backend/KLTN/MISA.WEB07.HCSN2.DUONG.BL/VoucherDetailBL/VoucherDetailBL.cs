using KLTN.Common.Entity;
using KLTN.DataLayer;
using KLTN.BussinesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN.BussinesLayer
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