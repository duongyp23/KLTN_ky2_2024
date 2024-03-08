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
    public class PropertyTypeBL : BaseBL<PropertyType>, IPropertyTypeBL
    {
        #region Field

        private IPropertyTypeDL _propertyTypeDL;

        #endregion

        #region Constructor

        public PropertyTypeBL(IPropertyTypeDL propertyTypeDL) : base(propertyTypeDL)
        {
            _propertyTypeDL = propertyTypeDL;
        }

        #endregion
    }
}
