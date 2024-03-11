using Dapper;
using KLTN.Common.Entity;
using KLTN.DataLayer;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN.DataLayer
{
    public class VoucherDetailDL : BaseDL<VoucherDetail>, IVoucherDetailDL
    {
        #region ConnectString
        // đường dẫn kết nối với DB
        private const string CONNECTION_STRING = "Server=localhost;Port=3307;Database=KLTN;Uid=root;Pwd=yp2382001;";

        #endregion
        #region method

        /// <summary>
        /// Xóa voucher detail
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        public int DeleteVoucherDetail(Guid voucherID)
        {
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            // Thêm tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@VoucherID", voucherID);



            String updateProperty = "UPDATE property p SET p.IsActive = 0 WHERE p.PropertyID IN ( SELECT v.PropertyID FROM voucherdetail v WHERE v.VoucherID = @VoucherID);";

            mySqlConnection.Execute(updateProperty, parameters);

            String deleteVoucherDetail = "DELETE FROM voucherdetail WHERE VoucherID = @VoucherID";

            var number = mySqlConnection.Execute(deleteVoucherDetail, parameters);

            return number;
        }

        /// <summary>
        /// Thêm mới voucherdetail
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="propertyID"></param>
        /// <returns></returns>
        public int InsertVoucherDetail(Guid voucherID, Guid propertyID)
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            //tạo câu lệnh truy vấn
            String insertPropertyCommand = "INSERT INTO voucherdetail (VoucherID, PropertyID, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy) " +
                "VALUES(@VoucherID, @PropertyID, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy);";

            // tạo param
            var parameters = new DynamicParameters();

            parameters.Add("@VoucherID", voucherID);
            parameters.Add("@PropertyID", propertyID);
            parameters.Add("@CreatedDate", DateTime.Now);
            parameters.Add("@CreatedBy", "admin");
            parameters.Add("@ModifiedDate", DateTime.Now);
            parameters.Add("@ModifiedBy", "admin");

            // chạy câu lệnh
            var result = mySqlConnection.Execute(insertPropertyCommand, parameters);
            return result;
        }
        #endregion
    }
}