using KLTN.Common.Entity;
using KLTN.DataLayer;

namespace KLTN.BussinesLayer
{
    public class CategoryBL : BaseBL<Category>, ICategoryBL
    {
        #region Field

        private ICategoryDL _categoryDL;

        #endregion

        #region Constructor

        public CategoryBL(ICategoryDL categoryDL) : base(categoryDL)
        {
            _categoryDL = categoryDL;
        }

        #endregion
    }
}