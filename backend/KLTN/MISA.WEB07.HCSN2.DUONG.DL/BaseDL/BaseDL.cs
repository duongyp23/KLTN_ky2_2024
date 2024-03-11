using Dapper;
using KLTN.Common.Entity.DTO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KLTN.DataLayer
{
    public class BaseDL<T> : IBaseDL<T>
    {

        #region Field
        
        //kết nối db
        private const string CONNECTION_STRING = "Server=localhost;Port=3307;Database=rental_store;Uid=root;Pwd=yp2382001;"; 


        #endregion


        /// <summary>
        /// lấy tất cả bản ghi
        /// </summary>
        public virtual IEnumerable<dynamic> GetAllRecords()
        {
            using (var mySqlConnection = new MySqlConnection(CONNECTION_STRING))
            {
                //tạo câu lệnh truy vấn 
                string tableName = typeof(T).GetCustomAttribute<TableAttribute>().Name;
                String getAllRecordsCommand = $"SELECT * FROM {tableName};";

                // thực hiện chạy câu lệnh 
                var records = mySqlConnection.Query(getAllRecordsCommand);

                return records; 
            }
        }

        public virtual async Task<PagingData<T>> GetPaging(List<Filter>? filters, int pageSize, int pageNumber)
        {
            string tableName = typeof(T).GetCustomAttribute<TableAttribute>().Name;

            string whereClause = "";
            if (filters != null && filters.Count > 0)
            {
                whereClause += "WHERE ";
                for (int i = 0; i < filters.Count; i++)
                {
                    Filter filter = filters[i];
                    if (i > 0)
                    {
                        whereClause += " AND ";
                    }
                    whereClause += $"{filter.columnName} {filter.operatorValue} {filter.operatorValue} ";
                }
            }
            String getPagingData = $"SELECT * FROM {tableName} {whereClause} LIMIT  {(pageNumber-1)*pageSize}, {pageSize};";
            String getTotalData = $"SELECT COUNT(*) FROM {tableName} {whereClause};";
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            List<T> datas = (List<T>)await mySqlConnection.QueryAsync<T>(getPagingData);
            var total = await mySqlConnection.QueryAsync<int>(getTotalData);
            mySqlConnection.Close();
            return new PagingData<T>()
            {
                Data = datas,
                TotalCount = (int)total.First()
            };
        }

        public virtual async Task<Guid> Insert(T entity)
        {
            string tableName = typeof(T).GetCustomAttribute<TableAttribute>().Name;

            var sb = new StringBuilder();
            var listCol = new StringBuilder();
            var listValue = new StringBuilder();
            var param = new DynamicParameters();
            Guid id = Guid.NewGuid();
            foreach (var property in typeof(T).GetProperties()) 
            {
                if (property.GetCustomAttribute<KeyAttribute>() != null)
                {
                    property.SetValue(entity, id);
                }
                listCol.AppendFormat($", {property.Name}");
                listValue.AppendFormat($", @{property.Name}");
                param.Add($"@{property.Name}", property.GetValue(entity));
            }
            listCol.Remove(0, 1);
            listValue.Remove(0, 1);
            sb.AppendFormat($"INSERT INTO {tableName} ({listCol}) VALUE ({listValue});");
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            await mySqlConnection.ExecuteAsync(sb.ToString(), param);
            mySqlConnection.Close();
            return id;
        }
    }
}
