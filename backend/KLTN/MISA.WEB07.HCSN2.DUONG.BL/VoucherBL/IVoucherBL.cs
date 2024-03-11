using KLTN.Common.Entity;
using KLTN.BussinesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLTN.Common.Entity.DTO;

namespace KLTN.BussinesLayer
{
    public interface IVoucherBL : IBaseBL<Voucher>
    {

        /// <summary>
        /// lấy danh sách Tài sản và tổng số bản ghi
        /// </summary>
        /// <param name="keyword">điều kiện lọc</param>
        /// <param name="pageSize">số item trong 1 trang</param>
        /// <param name="pageNumber">số trang muốn lấy </param>
        /// <returns>danh sách tài sản và tổng số bản ghi</returns>
        /// NTD 22/8/2022
        public PagingData<Voucher> GetPaging(
             String? keyword,
             int pageSize,
             int pageNumber);
        /// <summary>
        /// Lấy danh sách tài sản trong voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        public PagingData<Property> GetPropertyInVoucher(Guid voucherID);

        /// <summary>
        /// lấy new code
        /// </summary>
        /// <returns></returns>
        /// NTD 22/8/2022
        public String GetNewVoucherCode();

        /// <summary>
        /// lấy thông tin 1 voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns>Thông tin tài sản</returns>
        /// NTD 22/8/2022
        public Object GetVoucherByID(Guid voucherID);

        /// <summary>
        /// hàm xóa voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// NTD 22/8/2022
        public int DeleteVoucherByID(Guid voucherID);

        /// <summary>
        /// hàm thêm mới 1 voucher
        /// </summary>
        /// <param name="voucher"></param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// NTD 22/8/2022

        public Guid InsertVoucher(NewVoucherData newVoucher);

        /// <summary>
        /// Cập nhật 1 voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="voucher"></param>
        /// <returns></returns>
        public int UpdateVoucher(Guid voucherID, NewVoucherData voucher);

        /// <summary>
        /// Xóa nhiều voucher
        /// </summary>
        /// <param name="listVoucherID"></param>
        /// <returns></returns>
        public int DeleteMultipleVoucher(List<Guid> listVoucherID);
    }
}