using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.Common.Entity.DTO;
using MISA.WEB07.HCSN2.DUONG.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.HCSN2.DUONG.DL
{
    public interface IVoucherDL : IBaseDL<Voucher>
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
        /// Lấy danh sách taifd sản của voucher
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
        /// lấy thông tin 1 Voucher
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
        /// Thêm mới 1 voucher
        /// </summary>
        /// <param name="voucher"></param>
        /// <returns></returns>
        public Guid InsertVoucher(Voucher voucher);

        /// <summary>
        /// Cập nhật 1 voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="voucher"></param>
        /// <returns></returns>
        public int UpdateVoucher(Guid voucherID, Voucher voucher);

        /// <summary>
        /// hàm xóa voucher
        /// </summary>
        /// <param name="listVoucherID"></param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// NTD 22/8/2022
        public int DeleteMultipleVoucher(List<Guid> listVoucherID);

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="voucherCode"></param>
        /// <returns>true/false dựa theo mã bị trùng hay không</returns>
        public Guid CheckVoucherCode(String voucherCode);
    }
}