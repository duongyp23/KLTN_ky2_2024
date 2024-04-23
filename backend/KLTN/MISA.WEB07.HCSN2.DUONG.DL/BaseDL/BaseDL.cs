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
using static Dapper.SqlMapper;

namespace KLTN.DataLayer
{
    public class BaseDL<T> : IBaseDL<T>
    {

        #region Field
        
        //kết nối db
        public const string CONNECTION_STRING = "Server=localhost;Port=3307;Database=rental_store;Uid=root;Pwd=yp2382001;";

        


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
                    whereClause += $"{filter.columnName} {filter.operatorValue} {filter.filterValue} ";
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
                if (property.GetValue(entity) != null)
                {
                    listCol.AppendFormat($", {property.Name}");
                    listValue.AppendFormat($", @{property.Name}");
                    param.Add($"@{property.Name}", property.GetValue(entity));
                }
            }
            listCol.Remove(0, 1);
            listValue.Remove(0, 1);
            sb.AppendFormat($"INSERT INTO {tableName} ({listCol}) VALUE ({listValue});");
            try
            {
                var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

                await mySqlConnection.ExecuteAsync(sb.ToString(), param);
                mySqlConnection.Close();
            }
            catch
            {
                id = Guid.Empty;
            }
            
            return id;
        }

        public virtual async Task<bool> Update(T entity)
        {
            string tableName = typeof(T).GetCustomAttribute<TableAttribute>().Name;

            var sb = new StringBuilder();
            var listUpdate = new StringBuilder();
            var where = new StringBuilder();
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.GetCustomAttribute<KeyAttribute>() != null)
                {
                    where.AppendFormat($" {property.Name} = @{property.Name} ");
                } else if (property.GetValue(entity) != null)
                {
                    listUpdate.AppendFormat($" {property.Name} = @{property.Name} ,");
                }
            }
            listUpdate.Remove(listUpdate.Length -1, 1);
            sb.AppendFormat($"UPDATE {tableName} SET {listUpdate} WHERE {where};");
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            var result = await mySqlConnection.ExecuteAsync(sb.ToString(), entity);
            mySqlConnection.Close();
            return result == 1 ? true: false;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            string tableName = typeof(T).GetCustomAttribute<TableAttribute>().Name;

            var sb = new StringBuilder();
            var where = new StringBuilder();
            var param = new DynamicParameters();
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.GetCustomAttribute<KeyAttribute>() != null)
                {
                    where.AppendFormat($" {property.Name} = @{property.Name} ");
                    param.Add($"@{property.Name}", id);
                    break;
                }
            }
            sb.AppendFormat($"DELETE FROM {tableName} WHERE {where};");
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            var result = await mySqlConnection.ExecuteAsync(sb.ToString(), param);
            mySqlConnection.Close();
            return result == 1 ? true : false;
        }

        public async Task<List<T>> GetDataByField(List<Filter>? filters)
        {
            string tableName = typeof(T).GetCustomAttribute<TableAttribute>().Name;

            string whereClause = "";
            var param = new DynamicParameters();
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
                    whereClause += $"{filter.columnName} {filter.operatorValue} {filter.filterValue} ";
                }
            }
            String getPagingData = $"SELECT * FROM {tableName} {whereClause};";
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            List<T> datas = (List<T>)await mySqlConnection.QueryAsync<T>(getPagingData, param);
            mySqlConnection.Close();
            return datas;
        }

        public virtual async Task<T> GetDataById(Guid id)
        {
            string tableName = typeof(T).GetCustomAttribute<TableAttribute>().Name;

            var sb = new StringBuilder();
            var where = new StringBuilder();
            var param = new DynamicParameters();
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.GetCustomAttribute<KeyAttribute>() != null)
                {
                    where.AppendFormat($" {property.Name} = @{property.Name} ");
                    param.Add($"@{property.Name}", id);
                    break;
                }
            }
            sb.AppendFormat($"SELECT * FROM {tableName} WHERE {where};");
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            List<T> result = (List<T>)await mySqlConnection.QueryAsync<T>(sb.ToString(), param);
            mySqlConnection.Close();
            return result[0] ;
        }
    }
}
