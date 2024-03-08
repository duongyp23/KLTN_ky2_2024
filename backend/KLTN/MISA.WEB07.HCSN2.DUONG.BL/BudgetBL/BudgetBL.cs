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
    public class BudgetBL : BaseBL<Budget>, IBudgetBL
    {
        #region Field

        private DL.IBudgetDL _budgetDL;

        #endregion

        #region Constructor

        public BudgetBL(DL.IBudgetDL budgetDL) : base(budgetDL)
        {
            _budgetDL = budgetDL;
        }

        #endregion
    }
}