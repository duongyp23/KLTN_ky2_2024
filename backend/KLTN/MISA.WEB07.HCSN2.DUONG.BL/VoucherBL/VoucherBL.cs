using KLTN.BussinesLayer.PropertyBL;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using KLTN.Common.Enums;
using KLTN.DataLayer;
using KLTN.DataLayer.PropertyDL;

namespace KLTN.BussinesLayer
{
    public class VoucherBL : BaseBL<Voucher>, IVoucherBL
    {
        #region Field

        private IVoucherDL _voucherDL;
        private IPropertyDL _propertyDL;
        private IVoucherDetailDL _voucherDetailDL;
        private IPropertyBL _propertyBL;
        private IVoucherDetailBL _voucherDetailBL;
        #endregion

        #region Constructor

        public VoucherBL(IVoucherDL voucherDL, IPropertyBL propertyBL, IVoucherDetailBL voucherDetailBL,IPropertyDL propertyDL,IVoucherDetailDL voucherDetailDL) : base(voucherDL)
        {
            _voucherDL = voucherDL;
            _propertyBL = propertyBL;
            _voucherDetailBL = voucherDetailBL;
            _propertyDL = propertyDL;
            _voucherDetailDL = voucherDetailDL;
        }

        #endregion
        #region method 
        /// <summary>
        /// Lấy danh sách tài sản
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public PagingData<Voucher> GetPaging(string? keyword, int pageSize, int pageNumber)
        {

            return _voucherDL.GetPaging(keyword, pageSize, pageNumber);
        }
        /// <summary>
        /// Lấy danh sách tài sản của voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        public PagingData<Property> GetPropertyInVoucher(Guid voucherID)
        {

            return _voucherDL.GetPropertyInVoucher(voucherID);
        }

        /// <summary>
        /// lấy new code
        /// </summary>
        /// <returns></returns>
        /// NTD 22/8/2022
        public String GetNewVoucherCode()
        {
            return _voucherDL.GetNewVoucherCode();
        }

        /// <summary>
        /// lấy thông tin 1 voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns>Thông tin tài sản</returns>
        /// NTD 22/8/2022
        public Object GetVoucherByID(Guid voucherID)
        {
            return _voucherDL.GetVoucherByID(voucherID);
        }

        /// <summary>
        /// hàm xóa voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// NTD 22/8/2022
        public int DeleteVoucherByID(Guid voucherID)
        {
            return _voucherDL.DeleteVoucherByID(voucherID);
        }
        /// <summary>
        /// Thêm mới voucher
        /// </summary>
        /// <param name="newVoucher"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Guid InsertVoucher(NewVoucherData newVoucher)
        {
            Guid checkVoucherID = _voucherDL.CheckVoucherCode(newVoucher.VoucherData.VoucherCode);
            if(checkVoucherID != Guid.Empty)
            {
                throw new Exception(ErrorCode.DuplicateCode.ToString());
            }
            else
            {
                ///check từng property trong voucher
                for (int i = 0; i < newVoucher.ListProperty.Count; i++)
                {
                    _propertyBL.CheckProperty(newVoucher.ListProperty[i]);
                    Guid checkPropertyID = _propertyDL.CheckPropertyCode(newVoucher.ListProperty[i].PropertyCode);
                    if(checkPropertyID != newVoucher.ListProperty[i].PropertyID)
                    {
                        throw new Exception(ErrorCode.DuplicateCode.ToString());
                    }
                }
                ///Thêm mới voucher
                Guid id = _voucherDL.InsertVoucher(newVoucher.VoucherData);
                for (int i = 0; i < newVoucher.ListProperty.Count; i++)
                {
                    _voucherDetailDL.InsertVoucherDetail(id, newVoucher.ListProperty[i].PropertyID);
                    _propertyDL.UpdateProperty(newVoucher.ListProperty[i].PropertyID, newVoucher.ListProperty[i]);
                }
                return id;
            }
            
        }
        /// <summary>
        /// Sửa thông tin voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="voucher"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int UpdateVoucher(Guid voucherID, NewVoucherData voucher)
        {
            Guid checkVoucherID = _voucherDL.CheckVoucherCode(voucher.VoucherData.VoucherCode);
            if (checkVoucherID == Guid.Empty)
            {
                ///check từng property trong voucher
                for (int i = 0; i < voucher.ListProperty.Count; i++)
                {
                    _propertyBL.CheckProperty(voucher.ListProperty[i]);
                    Guid checkPropertyID = _propertyDL.CheckPropertyCode(voucher.ListProperty[i].PropertyCode);
                    if (checkPropertyID != voucher.ListProperty[i].PropertyID)
                    {
                        throw new Exception(ErrorCode.DuplicateCode.ToString());
                    }
                }
                ///cập nhật voucher, voucher detail, property
                var number = _voucherDL.UpdateVoucher(voucherID, voucher.VoucherData);
                _voucherDetailDL.DeleteVoucherDetail(voucherID);
                for (int i = 0; i < voucher.ListProperty.Count; i++)
                {
                    _voucherDetailDL.InsertVoucherDetail(voucherID, voucher.ListProperty[i].PropertyID);
                    _propertyDL.UpdateProperty(voucher.ListProperty[i].PropertyID, voucher.ListProperty[i]);
                }
                return number;
            }
            else
            {
                if(checkVoucherID == voucherID)
                {
                    ///check từng property trong voucher
                    for (int i = 0; i < voucher.ListProperty.Count; i++)
                    {
                        _propertyBL.CheckProperty(voucher.ListProperty[i]);
                        Guid checkPropertyID = _propertyDL.CheckPropertyCode(voucher.ListProperty[i].PropertyCode);
                        if (checkPropertyID != voucher.ListProperty[i].PropertyID)
                        {
                            throw new Exception(ErrorCode.DuplicateCode.ToString());
                        }
                    }
                    ///cập nhật voucher, voucher detail, property
                    var number = _voucherDL.UpdateVoucher(voucherID, voucher.VoucherData);
                    _voucherDetailBL.DeleteVoucherDetail(voucherID);
                    for (int i = 0; i < voucher.ListProperty.Count; i++)
                    {
                        _voucherDetailBL.InsertVoucherDetail(voucherID, voucher.ListProperty[i].PropertyID);
                        _propertyBL.UpdateProperty(voucher.ListProperty[i].PropertyID, voucher.ListProperty[i]);
                    }
                    return number;
                }
                else
                {
                    throw new Exception(ErrorCode.DuplicateCode.ToString());
                }
            }
        }
        /// <summary>
        /// Xóa nhiều voucher
        /// </summary>
        /// <param name="listVoucherID"></param>
        /// <returns></returns>
        public int DeleteMultipleVoucher(List<Guid> listVoucherID)
        {
            return _voucherDL.DeleteMultipleVoucher(listVoucherID);
        }
        #endregion
    }
}