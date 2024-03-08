using MISA.WEB07.HCSN2.DUONG.Common;
using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.Common.Entity.DTO;
using MISA.WEB07.HCSN2.DUONG.Common.Enums;
using MISA.WEB07.HCSN2.DUONG.DL.PropertyDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.HCSN2.DUONG.BL.PropertyBL
{

    public class PropertyBL : BaseBL<Property>, IPropertyBL
    {

        #region Field

        private IPropertyDL _propertyDL;

        #endregion

        #region Constructor

        public PropertyBL(IPropertyDL propertyDL) : base(propertyDL)
        {
            _propertyDL = propertyDL;
            
        }

        #endregion

        #region CheckProperty
        /// <summary>
        /// Kiểm tra tài sản thỏa màn các điều kiện hay không
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public void CheckProperty(Property property)
        {
            List<String> ErrorList = new List<String>();
            if(property.PropertyCode == "" || property.PropertyCode.Length > 20 || property.PropertyCode == null)
            {
                ErrorList.Add("Mã tài sản không hợp lệ");
            }
            if(property.PropertyName == "" || property.PropertyName.Length > 255 || property.PropertyName == null)
            {
                ErrorList.Add("Tên tài sản không hợp lệ");
            }
            if(property.PropertyTypeID == null)
            {
                ErrorList.Add("ID Loại tài sản không hợp lệ");
            }
            if(property.PropertyTypeCode == "" || property.PropertyTypeCode.Length > 20 || property.PropertyTypeCode == null)
            {
                ErrorList.Add("Mã loại tài sản không hợp lệ");
            }
            if(property.PropertyTypeName == "" || property.PropertyTypeName.Length > 255 || property.PropertyTypeName == null )
            {
                ErrorList.Add("Tên loại tài sản không hợp lệ");
            }
            if(property.DepartmentID == null)
            {
                ErrorList.Add("ID phòng ban không hợp lệ");
            }
            if(property.DepartmentCode == null || property.DepartmentCode == "" || property.DepartmentCode.Length > 20)
            {
                ErrorList.Add("Mã phòng ban không hợp lệ");
            }
            if(property.DepartmentName == "" || property.DepartmentName.Length > 255 || property.DepartmentName == null)
            {
                ErrorList.Add("Tên phòng ban không hợp lệ");
            }
            if(property.Quantity <= 0  )
            {
                ErrorList.Add("Số lượng không hợp lệ");
            }
            if(property.MarkedPrice <= 0)
            {
                ErrorList.Add("Nguyên giá không hợp lệ");
            }
            if(property.AttritionRate <= 0 || property.AttritionRate > 1 || property.AttritionRate * (property.UsedYear - 1) >= 1)
            {
                ErrorList.Add("Tỉ lệ hao mòn không hợp lệ");
            }
            if(property.AttritionValue > property.MarkedPrice || property.AttritionValue <0)
            {
                ErrorList.Add("Giá trị hao mòn không hợp lệ");
            }
            string error = "";
            if (ErrorList.Count > 0)
            {
                error += $"{string.Join(" , ", ErrorList)}";
                throw new Exception(error);
            } 
        }
        #endregion

        #region method
        /// <summary>
        /// Xóa 1 tài sản banwgf id
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns></returns>
        public int DeletePropertyByID(Guid propertyID)
        {
            return _propertyDL.DeletePropertyByID(propertyID);
        }
        /// <summary>
        /// Xóa nhiều tài sản
        /// </summary>
        /// <param name="ListPropertyID"></param>
        /// <returns></returns>
        public int DeleteMultipleProperty(List<Guid> ListPropertyID)
        {
            return _propertyDL.DeleteMultipleProperty(ListPropertyID);
        }
        /// <summary>
        /// Lấy mã mới
        /// </summary>
        /// <returns></returns>
        public string GetNewPropertyCode()   
        {
            return _propertyDL.GetNewPropertyCode();
        }
        /// <summary>
        /// Lấy danh sách tài sản
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentID"></param>
        /// <param name="propertyTypeID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public PagingData<Property> GetPaging(string? keyword, Guid? departmentID, Guid? propertyTypeID, int pageSize, int pageNumber)
        {
           
             return _propertyDL.GetPaging(keyword, departmentID, propertyTypeID, pageSize, pageNumber);
        }
        /// <summary>
        /// Lấy tài sản theo id
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns></returns>
        public Object GetPropertyByID(Guid propertyID)
        {
            return _propertyDL.GetPropertyByID(propertyID);
        }
        /// <summary>
        /// Thêm mới tài sản
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public int InsertProperty(Property property)
        {
            try
            {
                CheckProperty(property);
                Guid propertyID = _propertyDL.CheckPropertyCode(property.PropertyCode); 
               if (propertyID != Guid.Empty)
                {
                    throw new Exception(((int)ErrorCode.DuplicateCode).ToString());
                }
                else
                {
                    return _propertyDL.InsertProperty(property);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
            
        }
        /// <summary>
        /// Sửa tài sản
        /// </summary>
        /// <param name="propertyID"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public int UpdateProperty(Guid propertyID, Property property)
        {
            try
            {
                CheckProperty(property);
                Guid propertyValue = _propertyDL.CheckPropertyCode(property.PropertyCode);
                if (propertyValue != Guid.Empty)
                {
                    if(propertyValue == propertyID)
                    {
                        return _propertyDL.UpdateProperty(propertyID, property);
                    } 
                    else
                    {
                        throw new Exception(((int)ErrorCode.DuplicateCode).ToString());
                    }
                }
                else
                {
                    return _propertyDL.UpdateProperty(propertyID, property);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        /// <summary>
        /// thêm mới nhiều tài sản
        /// </summary>
        /// <param name="ListProperty"></param>
        /// <returns></returns>
        public int ImportMultipleProperty(List<Property> ListProperty)
        {
            List<String> ErrorList = new List<String>();
            int tmp = 1;
            for (int i=0;i< ListProperty.Count; i++)
            {
                
                try
                {
                    CheckProperty(ListProperty[i]);
                    if (_propertyDL.CheckPropertyCode(ListProperty[i].PropertyCode) != null)
                    {
                        throw new Exception("Mã tài sản đã tồn tại");
                    }
                }
                catch (Exception ex)
                {
                    ErrorList.Add("Tài sản thứ "+ (i+tmp) +" :" + ex.Message);
                    ListProperty.RemoveAt(i);
                    i--;
                    tmp++;
                } 
                
            }
            if (ListProperty.Count > 0)
            {
                return _propertyDL.ImportMultipleProperty(ListProperty);
            }
            else
            {
                String error = "";
                if(ErrorList.Count > 0)
                {
                    error += $"{string.Join("\n", ErrorList)}";
                }
                throw new Exception(error);
            } 
                
        }

        public PagingData<Property> GetPagingNotActive(string? keyword, int pageSize, int pageNumber, List<Guid>? listPropertyID)
        {
            return _propertyDL.GetPagingNotActive(keyword,pageSize,pageNumber,listPropertyID);
        }
        #endregion
    }
}
