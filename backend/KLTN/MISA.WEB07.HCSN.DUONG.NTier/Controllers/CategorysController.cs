using KLTN.BussinesLayer;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using KLTN.Common.Enums;
using KLTN.NTier.Base;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KLTN.NTier.Controllers
{
    public class CategorysController : BasesController<Category>
    {
        #region Field

        private ICategoryBL _categoryBL;

        #endregion

        #region Constructor

        public CategorysController(ICategoryBL categoryBL) : base(categoryBL)
        {
            _categoryBL = categoryBL;
        }

        #endregion

        #region Method

        #endregion
    }
}
