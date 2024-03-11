using System.Data.Common;
using System.Globalization;
using System.Text.RegularExpressions;
using Dapper;
using KLTN.Common.Command;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;
using MySqlConnector;

namespace KLTN.DataLayer.PropertyDL
{
    public class PropertyDL : BaseDL<Property>, IPropertyDL
    {
        #region ConnectString
        // đường dẫn kết nối với DB
        private const string CONNECTION_STRING = "Server=localhost;Port=3307;Database=KLTN;Uid=root;Pwd=yp2382001;";
        #endregion
        #region method
        /// <summary>
        /// hàm xóa 1 tài sản
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns>trả về ID tài sản vừa xóa</returns>
        /// NTD 22/8/2022
        public int DeletePropertyByID(Guid propertyID)
        {
            // tạo kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            //tạo câu lệnh truy vấn 
            //String deletePropertyCommand = "DELETE FROM property WHERE PropertyID = @PropertyID;";
            String deletePropertyCommand = PropertyCommand.Delete;

            // tạo param
            var parameters = new DynamicParameters();
            parameters.Add("@PropertyID", propertyID);

            // thực hiện chạy câu lệnh 
            var numberAffectedRows = mySqlConnection.Execute(deletePropertyCommand, parameters);

            return numberAffectedRows;
        }
        /// <summary>
        /// lấy new code
        /// </summary>
        /// <returns>Mã tài sản mới tự động tăng</returns>
        /// NTD 22/8/2022
        public string GetNewPropertyCode()
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            //tạo câu lệnh
            String getMaxPropertyCodeCommand = "SELECT PropertyCode FROM property ORDER BY PropertyCode DESC LIMIT 1;";

            // chạy câu lệnh 
            var maxPropertyCode = mySqlConnection.QueryFirstOrDefault<string>(getMaxPropertyCodeCommand);

            Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
            Match result = re.Match(maxPropertyCode);

            string alphaPart = result.Groups[1].Value;
            string numberPart = result.Groups[2].Value;
            var newPropertyCode = alphaPart + (Int64.Parse(numberPart) + 1).ToString();
            return newPropertyCode;
        }


        /// <summary>
        /// lấy thông tin 1 Tài sản
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns>Thông tin tài sản</returns>
        /// NTD 22/8/2022
        public Object GetPropertyByID(Guid propertyID)
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            //tạo câu lệnh 
            String getPropertyByIDCommand = "SELECT * FROM property WHERE PropertyID = @PropertyID;";

            // tạo param
            var parameters = new DynamicParameters();
            parameters.Add("@PropertyID", propertyID);

            //chạy câu lệnh 
            var property = mySqlConnection.QueryFirstOrDefault(getPropertyByIDCommand, parameters);

            return property;
        }
       
        /// <summary>
        /// thêm mới 1 tài sản
        /// </summary>
        /// <param name="property"></param>
        /// <returns>trả về id của tài sản </returns>
        /// NTD 22/8/2022
        public int InsertProperty(Property property)
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            //tạo câu lệnh truy vấn
            String insertPropertyCommand = "INSERT INTO property (PropertyID, PropertyCode, PropertyName, DepartmentID," +
                " DepartmentName, DepartmentCode, PropertyTypeID, PropertyTypeCode, PropertyTypeName, Quantity, MarkedPrice, " +
                "UsedYear, AttritionRate, AttritionValue, TrackingYear, PurchasingDate, StartUsingDate, IsActive, Budget, CreatedDate, CreatedBy, " +
                "ModifiedDate, ModifiedBy) " +
                "VALUES(@PropertyID, @PropertyCode, @PropertyName, @DepartmentID, @DepartmentName, @DepartmentCode, @PropertyTypeID, " +
                "@PropertyTypeCode, @PropertyTypeName, @Quantity, @MarkedPrice, @UsedYear, @AttritionRate, @AttritionValue, @TrackingYear," +
                " @PurchasingDate, @StartUsingDate,@IsActive,@Budget, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy);";

            // tạo param
            var parameters = new DynamicParameters();
            var propertyID = Guid.NewGuid();

            parameters.Add("@PropertyID", propertyID);
            parameters.Add("@PropertyCode", property.PropertyCode);
            parameters.Add("@PropertyName", property.PropertyName);
            parameters.Add("@DepartmentID", property.DepartmentID);
            parameters.Add("@DepartmentName", property.DepartmentName);
            parameters.Add("@DepartmentCode", property.DepartmentCode);
            parameters.Add("@PropertyTypeID", property.PropertyTypeID);
            parameters.Add("@PropertyTypeCode", property.PropertyTypeCode);
            parameters.Add("@PropertyTypeName", property.PropertyTypeName);
            parameters.Add("@Quantity", property.Quantity);
            parameters.Add("@MarkedPrice", property.MarkedPrice);
            parameters.Add("@UsedYear", property.UsedYear);
            parameters.Add("@AttritionRate", property.AttritionRate);
            parameters.Add("@AttritionValue", property.AttritionValue);
            parameters.Add("@TrackingYear", property.TrackingYear);
            parameters.Add("@PurchasingDate", property.PurchasingDate);
            parameters.Add("@StartUsingDate", property.StartUsingDate);
            parameters.Add("@IsActive", property.IsActive);
            parameters.Add("@Budget", property.Budget);
            parameters.Add("@CreatedDate", DateTime.Now);
            parameters.Add("@CreatedBy", "admin");
            parameters.Add("@ModifiedDate", DateTime.Now);
            parameters.Add("@ModifiedBy", "admin");

            // chạy câu lệnh
            var result = mySqlConnection.Execute(insertPropertyCommand, parameters);
            return result;
        }
        /// <summary>
        /// sửa 1 tài sản
        /// </summary>
        /// <param name="property"></param>
        /// <returns>trả về id của tài sản </returns>
        /// NTD 22/8/2022
        public int UpdateProperty(Guid propertyID, Property property)
        {
            // Kết nối DB

            MySqlConnection mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            // Tạo câu lệnh
            String updateEmployeeCommand = "UPDATE property SET " +
                "PropertyCode = @PropertyCode, " +
                "PropertyName = @PropertyName, " +
                "DepartmentID = @DepartmentID, " +
                "DepartmentName = @DepartmentName, " +
                "DepartmentCode = @DepartmentCode, " +
                "PropertyTypeID = @PropertyTypeID, " +
                "PropertyTypeCode = @PropertyTypeCode, " +
                "PropertyTypeName = @PropertyTypeName, " +
                "Quantity = @Quantity, " +
                "MarkedPrice = @MarkedPrice, " +
                "UsedYear = @UsedYear, " +
                "AttritionRate = @AttritionRate, " +
                "AttritionValue = @AttritionValue, " +
                "TrackingYear = @TrackingYear, " +
                "PurchasingDate = @PurchasingDate, " +
                "StartUsingDate = @StartUsingDate, " +
                "ModifiedDate = @ModifiedDate, " +
                "IsActive = @IsActive, " +
                "Budget = @Budget , " +
                "ModifiedBy = @ModifiedBy " +
                "WHERE PropertyID = @PropertyID;";
            // Thêm tham số đầu vào 
            var parameters = new DynamicParameters();

            parameters.Add("@PropertyID", propertyID);
            parameters.Add("@PropertyCode", property.PropertyCode);
            parameters.Add("@PropertyName", property.PropertyName);
            parameters.Add("@DepartmentID", property.DepartmentID);
            parameters.Add("@DepartmentName", property.DepartmentName);
            parameters.Add("@DepartmentCode", property.DepartmentCode);
            parameters.Add("@PropertyTypeID", property.PropertyTypeID);
            parameters.Add("@PropertyTypeCode", property.PropertyTypeCode);
            parameters.Add("@PropertyTypeName", property.PropertyTypeName);
            parameters.Add("@Quantity", property.Quantity);
            parameters.Add("@MarkedPrice", property.MarkedPrice);
            parameters.Add("@UsedYear", property.UsedYear);
            parameters.Add("@AttritionRate", property.AttritionRate);
            parameters.Add("@AttritionValue", property.AttritionValue);
            parameters.Add("@TrackingYear", property.TrackingYear);
            parameters.Add("@PurchasingDate", property.PurchasingDate);
            parameters.Add("@StartUsingDate", property.StartUsingDate);
            parameters.Add("@IsActive", property.IsActive);
            parameters.Add("@Budget", property.Budget);
            parameters.Add("@ModifiedDate", DateTime.Now);
            parameters.Add("@ModifiedBy", "admin");





            // Chạy câu lệnh
            var numberOfAffectedRows = mySqlConnection.Execute(updateEmployeeCommand, parameters);
            return numberOfAffectedRows;
        }

        /// <summary>
        /// hàm xóa nhiều tài sản
        /// </summary>
        /// <param name="ListPropertyID"></param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// NTD 22/8/2022
        public int DeleteMultipleProperty(List<Guid> ListPropertyID)
        {
            // kết nối database
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);
            // tạo param
            var parameters = new DynamicParameters();
            string listPropertyID = "";
            for (int i = 0; i < ListPropertyID.Count; i++)
            {
                listPropertyID += $" @PropertyID{i} ";

                if (i < ListPropertyID.Count - 1)
                {
                    listPropertyID += " , ";
                }
                parameters.Add($"@PropertyID{i}", ListPropertyID[i]);
            }
            // tạo câu lệnh và chạy câu lệnh
            string deleteMultiple = "DELETE FROM property WHERE PropertyID IN ( " + listPropertyID + "  );";
            int numberAffectedRows = mySqlConnection.Execute(deleteMultiple, parameters);
            return numberAffectedRows;
        }
        /// <summary>
        /// Thêm mới nhiều tài sản
        /// </summary>
        /// <param name="ListProperty"></param>
        /// <returns>Số lượng bản ghi thêm mới thành công</returns>
        public int ImportMultipleProperty(List<Property> ListProperty)
        {
            //tạo kết nối
            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);
            //tạo param
            string listPropertyCommand = "";
            var parameters = new DynamicParameters();
            for (int i = 0; i < ListProperty.Count; i++)
            {
                var propertyID = Guid.NewGuid();
                listPropertyCommand += $" (@PropertyID{i}, @PropertyCode{i}, @PropertyName{i}, @DepartmentID{i}, @DepartmentName{i}, @DepartmentCode{i}, @PropertyTypeID{i}," +
                    $"@PropertyTypeCode{i}, @PropertyTypeName{i}, @Quantity{i}, @MarkedPrice{i}, @UsedYear{i}, @AttritionRate{i}, @AttritionValue{i}, @TrackingYear{i}," +
                    $"@PurchasingDate{i}, @StartUsingDate{i}, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy)";
                parameters.Add($"@PropertyID{i}", propertyID);
                parameters.Add($"@PropertyCode{i}", ListProperty[i].PropertyCode);
                parameters.Add($"@PropertyName{i}", ListProperty[i].PropertyName);
                parameters.Add($"@DepartmentID{i}", ListProperty[i].DepartmentID);
                parameters.Add($"@DepartmentName{i}", ListProperty[i].DepartmentName);
                parameters.Add($"@DepartmentCode{i}", ListProperty[i].DepartmentCode);
                parameters.Add($"@PropertyTypeID{i}", ListProperty[i].PropertyTypeID);
                parameters.Add($"@PropertyTypeCode{i}", ListProperty[i].PropertyTypeCode);
                parameters.Add($"@PropertyTypeName{i}", ListProperty[i].PropertyTypeName);
                parameters.Add($"@Quantity{i}", ListProperty[i].Quantity);
                parameters.Add($"@MarkedPrice{i}", ListProperty[i].MarkedPrice);
                parameters.Add($"@UsedYear{i}", ListProperty[i].UsedYear);
                parameters.Add($"@AttritionRate{i}", ListProperty[i].AttritionRate);
                parameters.Add($"@AttritionValue{i}", ListProperty[i].AttritionValue);
                parameters.Add($"@TrackingYear{i}", ListProperty[i].TrackingYear);
                parameters.Add($"@PurchasingDate{i}", ListProperty[i].PurchasingDate);
                parameters.Add($"@StartUsingDate{i}", ListProperty[i].StartUsingDate);
                
                if (i < ListProperty.Count - 1)
                {
                    listPropertyCommand += " , ";
                }
            }
            parameters.Add("@CreatedDate", DateTime.Now);
            parameters.Add("@CreatedBy", "admin");
            parameters.Add("@ModifiedDate", DateTime.Now);
            parameters.Add("@ModifiedBy", "admin");

            //Tạo câu lệnh và chạy
            string insertMultiple = "INSERT INTO property (PropertyID, PropertyCode, PropertyName, DepartmentID," +
                " DepartmentName, DepartmentCode, PropertyTypeID, PropertyTypeCode, PropertyTypeName, Quantity, MarkedPrice, " +
                "UsedYear, AttritionRate, AttritionValue, TrackingYear, PurchasingDate, StartUsingDate, CreatedDate, CreatedBy, " +
                "ModifiedDate, ModifiedBy) " +
                "VALUES " + listPropertyCommand + " ;";
            int numberAffectedRows = mySqlConnection.Execute(insertMultiple, parameters);

            return numberAffectedRows;

        }

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="propertyCode"></param>
        /// <returns>true/false dựa theo mã bị trùng hay không</returns>
        public Guid CheckPropertyCode(String propertyCode)
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            //tạo câu lệnh 
            String getPropertyByCodeCommand = "SELECT PropertyID FROM property WHERE PropertyCode = @PropertyCode;";

            // tạo param
            var parameters = new DynamicParameters();
            parameters.Add("@PropertyCode", propertyCode);

            //chạy câu lệnh 
            
            Guid property = mySqlConnection.QueryFirstOrDefault<Guid>(getPropertyByCodeCommand, parameters);
            
            return property;
            
        }

        /// <summary>
        /// lấy danh sách tài sản chưa active
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="listPropertyID"></param>
        /// <returns></returns>
        
        public PagingData<Property> GetPagingNotActive(string? keyword, int pageSize, int pageNumber, List<Guid>? listPropertyID)
        {
            // Kết nối DB

            var mySqlConnection = new MySqlConnection(CONNECTION_STRING);

            // Tên Stored procedure cần chạy
            string storedProcedureName = "Proc_property_GetPaging";

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
                orConditions.Add($"PropertyName LIKE '%{keyword}%'");
                orConditions.Add($"PropertyCode LIKE '%{keyword}%'");
            }

            if (orConditions.Count > 0)
            {
                whereClause = $"({string.Join(" OR ", orConditions)})";
            }
            
            if (whereClause == "")
            {
                whereClause += " isActive = 0 ";
            }
            else
            {
                whereClause += " AND isActive = 0 ";
            }
            
            if (listPropertyID.Count > 0)
            {
                String stringListPropertyID = "";
                for (int i = 0; i < listPropertyID.Count; i++)
                {
                    stringListPropertyID += $" '{listPropertyID[i]}' ";

                    if (i < listPropertyID.Count - 1)
                    {
                        stringListPropertyID += " , ";
                    }
                }
                whereClause += " AND PropertyID  NOT IN ( " + stringListPropertyID + " )";
            }
            
            

            parameters.Add("v_Where", whereClause);

            // Chạy store procedure
            var multipleResults = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            var properties = multipleResults.Read<Property>().ToList();
            var totalCount = multipleResults.Read<int>().Single();
            return new PagingData<Property>()
            {
                Data = properties,
                TotalCount = totalCount
            };
        }
        #endregion
    }
}
