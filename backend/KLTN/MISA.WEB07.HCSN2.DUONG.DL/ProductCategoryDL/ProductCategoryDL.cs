using Dapper;
using KLTN.Common.Entity;
using MySqlConnector;

namespace KLTN.DataLayer
{
    public class ProductCategoryDL : BaseDL<ProductCategory>, IProductCategoryDL
    {
        public async Task RemoveOldCategory(Guid? product_id)
        {
            var param = new DynamicParameters();
            param.Add("@product_id", product_id);
            String queryString = $"DELETE FROM product_category where product_id = @product_id";
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            await mySqlConnection.ExecuteAsync(queryString, param);
            mySqlConnection.Close();
        }
    }
}