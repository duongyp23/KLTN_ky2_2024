using KLTN.BussinesLayer;
using KLTN.Common.Entity;
using KLTN.NTier.Base;

namespace KLTN.NTier.Controllers
{
    public class StocksController : BasesController<Stock>
    {
        #region Field

        private IStockBL _stockBL;

        #endregion

        #region Constructor

        public StocksController(IStockBL stockBL) : base(stockBL)
        {
            _stockBL = stockBL;
        }

        #endregion

        #region Method
        #endregion
    }
}
