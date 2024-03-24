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
        public async Task<Guid> GetWaitOrder(Guid userId)
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
                return (Guid)result[0].order_id;
            }
            return Guid.Empty;
        }
    }
}