using Dapper;
using MISA.WEB07.HCSN2.DUONG.Common.Entity;
using MISA.WEB07.HCSN2.DUONG.Common.Entity.DTO;
using MISA.WEB07.HCSN2.DUONG.DL;
using MISA.WEB07.HCSN2.DUONG.DL.PropertyDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WEB07.HCSN2.DUONG.DL
{
    public class VoucherDL : BaseDL<Voucher>, IVoucherDL
    {
        #region ConnectString
        // đường dẫn kết nối với DB
        private const string CONNECTION_STRING = "Server=localhost;Port=3307;Database=misa.web07.hcsn2.duong;Uid=root;Pwd=yp2382001;";
        #endregion
        #region method
        /// <summary>
        /// lấy danh sách Tài sản và tổng số bản ghi
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns>danh sách tài sản và tổng số bản ghi</returns>
        /// NTD 22/8/2022
        public PagingData<Voucher> GetPaging(
            String? keyword,
             int pageSize,
             int pageNumber)
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            // Tên Stored procedure cần chạy
            string storedProcedureName = "Proc_voucher_GetPaging";

            // Thêm tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("v_Offset", (pageNumber - 1) * pageSize);
            parameters.Add("v_Limit", pageSize);
            parameters.Add("v_Sort", "ModifiedDate DESC");

            var orConditions = new List<string>();
            var andConditions = new List<string>();
            string whereClause = "";

            if (keyword != null)
            {
                orConditions.Add($"Description LIKE '%{keyword}%'");
                orConditions.Add($"VoucherCode LIKE '%{keyword}%'");
            }

            if (orConditions.Count > 0)
            {
                whereClause = $"({string.Join(" OR ", orConditions)})";
            }

            parameters.Add("v_Where", whereClause);

            // Chạy store procedure
            var multipleResults = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            var vouchers = multipleResults.Read<Voucher>().ToList();
            var totalCount = multipleResults.Read<long>().Single();
            return new PagingData<Voucher>()
            {
                Data = vouchers,
                TotalCount = totalCount
            };
        }

        /// <summary>
        /// Lấy danh sách tài sản của voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns></returns>
        public PagingData<Property> GetPropertyInVoucher(Guid voucherID)
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            String getPropertyInVoucher = "SELECT * FROM property p WHERE p.PropertyID IN (SELECT v.PropertyID  FROM voucherdetail v  WHERE v.VoucherID = @VoucherID );";

            var parameters = new DynamicParameters();
            parameters.Add("@VoucherID", voucherID);

            //chạy câu lệnh 
            var multipleResults = mySqlConnection.QueryMultiple(getPropertyInVoucher, parameters);

            var properties = multipleResults.Read<Property>().ToList();
            
            return new PagingData<Property>()
            {
                Data = properties,
                
            };

        }

        /// <summary>
        /// lấy new code
        /// </summary>
        /// <returns>Mã tài sản mới tự động tăng</returns>
        /// NTD 22/8/2022
        public string GetNewVoucherCode()
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            //tạo câu lệnh
            String getMaxVoucherCodeCommand = "SELECT VoucherCode FROM voucher ORDER BY VoucherCode DESC LIMIT 1;";

            // chạy câu lệnh 
            var maxVoucherCode = mySqlConnection.QueryFirstOrDefault<string>(getMaxVoucherCodeCommand);

            Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
            Match result = re.Match(maxVoucherCode);

            string alphaPart = result.Groups[1].Value;
            string numberPart = result.Groups[2].Value;
            var newVoucherCode = alphaPart + (Int64.Parse(numberPart) + 1).ToString();
            return newVoucherCode;
        }


        /// <summary>
        /// lấy thông tin 1 Voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns>Thông tin tài sản</returns>
        /// NTD 22/8/2022
        public Object GetVoucherByID(Guid voucherID)
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            //tạo câu lệnh 
            String getVoucherByIDCommand = "SELECT * FROM voucher WHERE VoucherID = @VoucherID;";

            // tạo param
            var parameters = new DynamicParameters();
            parameters.Add("@VoucherID", voucherID);

            //chạy câu lệnh 
            var voucher = mySqlConnection.QueryFirstOrDefault(getVoucherByIDCommand, parameters);

            return voucher;
        }


        /// <summary>
        /// hàm xóa voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// NTD 22/8/2022
        public int DeleteVoucherByID(Guid voucherID)
        {
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);


            String updateProperty = "UPDATE property p SET p.IsActive = 0 WHERE p.PropertyID IN ( SELECT v.PropertyID FROM voucherdetail v WHERE v.VoucherID = @VoucherID);";

            String deleteVoucherDetail = "DELETE FROM voucherdetail WHERE VoucherID = @VoucherID";

            String deleteVoucher = "DELETE FROM voucher WHERE VoucherID = @VoucherID";

            mySqlConnection.Open();

            MySqlCommand myCommand = mySqlConnection.CreateCommand();
            MySqlTransaction myTrans;

            // Start a local transaction
            myTrans = mySqlConnection.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = mySqlConnection;
            myCommand.Transaction = myTrans;
            int number = 0;
            try
            {
                myCommand.Parameters.AddWithValue("@VoucherID", voucherID);
                myCommand.CommandText = updateProperty;
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = deleteVoucherDetail;
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = deleteVoucher;
                number = myCommand.ExecuteNonQuery();
                myTrans.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException ex)
                {
                }
            }
            finally
            {
                mySqlConnection.Close();
            }
            return number;
        }

        /// <summary>
        /// hàm xóa voucher
        /// </summary>
        /// <param name="listVoucherID"></param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// NTD 22/8/2022
        public int DeleteMultipleVoucher(List<Guid> listVoucherID)
        {
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            mySqlConnection.Open();

            MySqlCommand myCommand = mySqlConnection.CreateCommand();
            MySqlTransaction myTrans;

            // Start a local transaction
            myTrans = mySqlConnection.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = mySqlConnection;
            myCommand.Transaction = myTrans;
            int number = 0;
            try
            {
                string stringListVoucherID = "";
                for (int i = 0; i < listVoucherID.Count; i++)
                {
                    stringListVoucherID += $" @VoucherID{i} ";

                    if (i < listVoucherID.Count - 1)
                    {
                        stringListVoucherID += " , ";
                    }
                    myCommand.Parameters.AddWithValue($"@VoucherID{i}", listVoucherID[i]);
                }


                String updateProperty = "UPDATE property p SET p.IsActive = 0 WHERE p.PropertyID IN ( SELECT v.PropertyID FROM voucherdetail v WHERE v.VoucherID IN  (" + stringListVoucherID + "  ) );";

                String deleteVoucherDetail = "DELETE FROM voucherdetail WHERE VoucherID IN ( " + stringListVoucherID + " );";

                String deleteVoucher = "DELETE FROM voucher WHERE VoucherID IN (" + stringListVoucherID + " );";
                myCommand.CommandText = updateProperty;
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = deleteVoucherDetail;
                myCommand.ExecuteNonQuery();
                myCommand.CommandText = deleteVoucher;
                number = myCommand.ExecuteNonQuery();
                myTrans.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException ex)
                {
                }
            }
            finally
            {
                mySqlConnection.Close();
            }
            return number;



        }



        /// <summary>
        /// Thêm mới 1 voucher
        /// </summary>
        /// <param name="voucher"></param>
        /// <returns></returns>
        public Guid InsertVoucher(Voucher voucher)
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            //tạo câu lệnh truy vấn
            String insertVoucherCommand = "INSERT INTO voucher (VoucherID, VoucherCode, IncrementDate, StartUsingDate, TotalPrice, Description, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy )" +
                "VALUES(@VoucherID, @VoucherCode, @IncrementDate, @StartUsingDate, @TotalPrice, @Description, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy) ";

            // tạo param
            var parameters = new DynamicParameters();
            var voucherID = Guid.NewGuid();

            parameters.Add("@VoucherID", voucherID);
            parameters.Add("@VoucherCode", voucher.VoucherCode);
            parameters.Add("@IncrementDate", voucher.IncrementDate);
            parameters.Add("@StartUsingDate", voucher.StartUsingDate);
            parameters.Add("@TotalPrice", voucher.TotalPrice);
            parameters.Add("@Description", voucher.Description);
            parameters.Add("@CreatedDate", DateTime.Now);
            parameters.Add("@CreatedBy", "admin");
            parameters.Add("@ModifiedDate", DateTime.Now);
            parameters.Add("@ModifiedBy", "admin");

            // chạy câu lệnh
            mySqlConnection.Execute(insertVoucherCommand, parameters);
            return voucherID;
        }
        /// <summary>
        /// Cập nhật voucher
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="voucher"></param>
        /// <returns></returns>
        public int UpdateVoucher(Guid voucherID, Voucher voucher)
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            //tạo câu lệnh truy vấn
            String insertVoucherCommand = "UPDATE voucher " +
                "SET VoucherCode = @VoucherCode , " +
                "IncrementDate = @IncrementDate , " +
                "StartUsingDate = @StartUsingDate , " +
                "TotalPrice = @TotalPrice , " +
                "Description = @Description , " +
                "ModifiedDate = @ModifiedDate , " +
                "ModifiedBy = @ModifiedBy " +
                "WHERE VoucherID = @VoucherID ;";

            // tạo param
            var parameters = new DynamicParameters();

            parameters.Add("@VoucherID", voucherID);
            parameters.Add("@VoucherCode", voucher.VoucherCode);
            parameters.Add("@IncrementDate", voucher.IncrementDate);
            parameters.Add("@StartUsingDate", voucher.StartUsingDate);
            parameters.Add("@TotalPrice", voucher.TotalPrice);
            parameters.Add("@Description", voucher.Description);
            parameters.Add("@ModifiedDate", DateTime.Now);
            parameters.Add("@ModifiedBy", "admin");

            // chạy câu lệnh
            var number = mySqlConnection.Execute(insertVoucherCommand, parameters);
            return number;
        }

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="voucherCode"></param>
        /// <returns>true/false dựa theo mã bị trùng hay không</returns>
        public Guid CheckVoucherCode(String voucherCode)
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            //tạo câu lệnh 
            String getVoucherByCodeCommand = "SELECT VoucherID FROM voucher WHERE VoucherCode = @VoucherCode;";

            // tạo param
            var parameters = new DynamicParameters();
            parameters.Add("@VoucherCode", voucherCode);

            //chạy câu lệnh 

            Guid voucherID = mySqlConnection.QueryFirstOrDefault<Guid>(getVoucherByCodeCommand, parameters);

            return voucherID;

        }
        #endregion
    }
}