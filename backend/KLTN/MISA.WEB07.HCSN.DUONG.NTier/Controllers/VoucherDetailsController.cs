﻿using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.HCSN2.DUONG.BL;
using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.Common.Enums;
using MISA.WEB07.HCSN2.DUONG.NTier.Base;

namespace MISA.WEB07.HCSN.DUONG.NTier.Controllers
{
    public class VoucherDetailsController : BasesController<VoucherDetail>
    {
        #region Field

        private IVoucherDetailBL _voucherDetailBL;

        #endregion

        #region Constructor

        public VoucherDetailsController(IVoucherDetailBL voucherDetailBL) : base(voucherDetailBL)
        {
            _voucherDetailBL = voucherDetailBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>

        [HttpGet]
        public override IActionResult GetAllRecords()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _voucherDetailBL.GetAllRecords());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, ErrorCode.Exception);
            }
        }

        #endregion
    }
}
