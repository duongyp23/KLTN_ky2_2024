using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using KLTN.Common.Entity;
using MySqlConnector;

namespace KLTN.DataLayer
{
    public class OrderDL : BaseDL<Order>, IOrderDL
    {
        public async Task<List<Product>> GetProductInOrder(Guid id)
        {
            var param = new DynamicParameters();
            param.Add("@order_id", id);
            String queryString = $"SELECT * FROM product WHERE product_id in (SELECT product_id FROM order_detail WHERE order_id = @order_id)";
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            List<Product> products = (List<Product>)await mySqlConnection.QueryAsync<Product>(queryString, param);
            mySqlConnection.Close();
            return products;
        }

        public async Task<Order> GetWaitOrder(Guid userId)
        {

            var sb = new StringBuilder();
            var param = new DynamicParameters();
            param.Add("@user_id", userId);
            sb.AppendFormat($"SELECT * FROM orders WHERE status = 1 and user_id = @user_id;");
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            List<Order> result = (List<Order>)await mySqlConnection.QueryAsync<Order>(sb.ToString(), param);
            mySqlConnection.Close();
            if(result != null && result.Count > 0)
            {
                return result[0];
            }
            return null;
        }

        public async Task UpdateQuantityProduct(Guid? product_id)
        {
            var param = new DynamicParameters();
            param.Add("@product_id", product_id);
            String queryString = $"UPDATE product p SET p.quantity = p.quantity - 1, p.quantity_rental = IFNULL(p.quantity_rental, 0) + 1 WHERE product_id = @product_id;";
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            await mySqlConnection.ExecuteAsync(queryString, param);
            mySqlConnection.Close();
        }
    }
}