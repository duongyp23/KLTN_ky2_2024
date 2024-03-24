using System.ComponentModel.DataAnnotations.Schema;
using Dapper;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using MySqlConnector;

namespace KLTN.DataLayer
{
    public class CategoryDL : BaseDL<Category>, ICategoryDL
    {
        public async Task<List<Category>> GetCategoryOfProduct(Guid productId)
        {
            var param = new DynamicParameters();
            param.Add("@product_id", productId);
            String getData = $"SELECT * FROM category where category_id in (select category_id from product_category where product_id = @product_id)";
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            List<Category> datas = (List<Category>)await mySqlConnection.QueryAsync<Category>(getData, param);
            mySqlConnection.Close();
            return datas;
        }
    }
}