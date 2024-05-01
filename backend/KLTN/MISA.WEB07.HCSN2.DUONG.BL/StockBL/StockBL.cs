using KLTN.Common.Entity;
using KLTN.DataLayer;

namespace KLTN.BussinesLayer
{
    public class StockBL : BaseBL<Stock>, IStockBL
    {
        #region Field

        private IStockDL _stockDL;

        #endregion

        #region Constructor

        public StockBL(IStockDL stockDL) : base(stockDL)
        {
            _stockDL = stockDL;
        }


        #endregion

        #region Method
        #endregion
    }
}