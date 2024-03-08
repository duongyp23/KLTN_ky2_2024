using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.HCSN2.DUONG.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {

        #region Field
        
        //kết nối db
        private const string CONNECTION_STRING = "Server=localhost;Port=3307;Database=misa.web07.hcsn2.duong;Uid=root;Pwd=yp2382001;"; 


        #endregion


        /// <summary>
        /// lấy tất cả bản ghi
        /// Author: TTDUC 24/08/2022
        /// </summary>
        /// <returns>tất cả bản ghi của 1 bảng</returns>
        public virtual IEnumerable<dynamic> GetAllRecords()
        {
            using (var mySqlConnection = new MySqlConnection(CONNECTION_STRING))
            {
                //tạo câu lệnh truy vấn 
                string className = typeof(T).Name;
                String getAllRecordsCommand = $"SELECT * FROM {className};";

                // thực hiện chạy câu lệnh 
                var records = mySqlConnection.Query(getAllRecordsCommand);

                return records; 
            }
        }
    }
}
