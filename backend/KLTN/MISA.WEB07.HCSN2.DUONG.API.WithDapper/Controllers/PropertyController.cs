using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.HCSN2.DUONG.API.WithDapper.Entity;
using MISA.WEB07.HCSN2.DUONG.API.WithDapper.Entity.DTO;
using MySqlConnector;
using Swashbuckle.AspNetCore.Annotations;

using MySqlErrorCode = MySqlConnector.MySqlErrorCode;
using MySqlException = MySqlConnector.MySqlException;

namespace MISA.WEB07.HCSN2.DUONG.API.WithDapper.Controllers
{
    [Route("api/v1/properties")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        /// <summary>
        /// thêm mới 1 tài sản
        /// </summary>
        /// <param name="property"></param>
        /// <returns>trả về id của tài sản </returns>
        /// NTD 22/8/2022
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult InsertProperty([FromBody] Property property)
        {
            try
            {
                // Kết nối DB
                String connectionString = "Server=localhost;Port=3307;Database=misa.web07.hcsn2.duong;Uid=root;Pwd=yp2382001;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //tạo câu lệnh truy vấn
                String insertPropertyCommand = "INSERT INTO property (PropertyID, PropertyCode, PropertyName, DepartmentID," +
                    " DepartmentName, DepartmentCode, PropertyTypeID, PropertyTypeCode, PropertyTypeName, Quantity, MarkedPrice, " +
                    "UsedYear, AttritionRate, AttritionValue, TrackingYear, PurchasingDate, StartUsingDate, CreatedDate, CreatedBy, " +
                    "ModifiedDate, ModifiedBy) " +
                    "VALUES(@PropertyID, @PropertyCode, @PropertyName, @DepartmentID, @DepartmentName, @DepartmentCode, @PropertyTypeID, " +
                    "@PropertyTypeCode, @PropertyTypeName, @Quantity, @MarkedPrice, @UsedYear, @AttritionRate, @AttritionValue, @TrackingYear," +
                    " @PurchasingDate, @StartUsingDate, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy);";

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
                parameters.Add("@CreatedDate", property.CreatedDate);
                parameters.Add("@CreatedBy", property.CreatedBy);
                parameters.Add("@ModifiedDate", property.ModifiedDate);
                parameters.Add("@ModifiedBy", property.ModifiedBy);

                // chạy câu lệnh
                var result = mySqlConnection.Execute(insertPropertyCommand, parameters);

                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, propertyID);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "e002");
            }
            catch (MySqlException mySqlException)
            {
                if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e003");
                }
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
            catch (Exception exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }


        }

        /// <summary>
        /// sửa 1 tài sản
        /// </summary>
        /// <param name="property"></param>
        /// <returns>trả về id của tài sản </returns>
        /// NTD 22/8/2022
        [HttpPut("{propertyID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateEmployee([FromRoute] Guid propertyID, [FromBody] Property property)
        {
            try
            {
                // Kết nối DB
                String connectionString = "Server=localhost;Port=3307;Database=misa.web07.hcsn2.duong;Uid=root;Pwd=yp2382001;";
                MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

                // Tạo câu lệnh
                String updateEmployeeCommand = "UPDATE property SET " +
                    "PropertyID = @PropertyID, " +
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
                    "CreatedDate = @CreatedDate, " +
                    "CreatedBy = @CreatedBy, " +
                    "ModifiedDate = @ModifiedDate, " +
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
                parameters.Add("@CreatedDate", property.CreatedDate);
                parameters.Add("@CreatedBy", property.CreatedBy);
                parameters.Add("@ModifiedDate", DateTime.Now);
                parameters.Add("@ModifiedBy", property.ModifiedBy);





                // Chạy câu lệnh
                var numberOfAffectedRows = mySqlConnection.Execute(updateEmployeeCommand, parameters);

                
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, propertyID);
                }
                else
                {
                    // Lỗi không UPDATE được 
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
            }
            catch (Exception exception)
            {
                // Lỗi ex chung
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }


        /// <summary>
        /// hàm xóa 1 tài sản
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns>trả về ID tài sản vừa xóa</returns>
        /// NTD 22/8/2022
        [HttpDelete("{propertyID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePropertyByID([FromRoute] Guid propertyID)
        {
            try
            {
                // Kết nối DB
                String connectionString = "Server=localhost;Port=3307;Database=misa.web07.hcsn2.duong;Uid=root;Pwd=yp2382001;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //tạo câu lệnh 
                String deletePropertyCommand = "DELETE FROM property WHERE PropertyID = @PropertyID;";

                // tạo param
                var parameters = new DynamicParameters();
                parameters.Add("@PropertyID", propertyID);

                // chạy câu lệnh 
                var result = mySqlConnection.Execute(deletePropertyCommand, parameters);

                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, propertyID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        /// <summary>
        /// lấy thông tin 1 Tài sản
        /// </summary>
        /// <param name="propertyID"></param>
        /// <returns>Thông tin tài sản</returns>
        /// NTD 22/8/2022
        [HttpGet("{propertyID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Property))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPropertyByID([FromRoute] Guid propertyID)
        {

            try
            {
                // Kết nối DB
                String connectionString = "Server=localhost;Port=3307;Database=misa.web07.hcsn2.duong;Uid=root;Pwd=yp2382001;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //tạo câu lệnh 
                String getPropertyByIDCommand = "SELECT * FROM property WHERE PropertyID = @PropertyID;";

                // tạo param
                var parameters = new DynamicParameters();
                parameters.Add("@PropertyID", propertyID);

                //chạy câu lệnh 
                var property = mySqlConnection.QueryFirstOrDefault(getPropertyByIDCommand, parameters);

                if (property != null)
                {
                    return StatusCode(StatusCodes.Status200OK, property);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }


        /// <summary>
        /// lấy danh sách Tài sản và tổng số bản ghi
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentID"></param>
        /// <param name="propertyTypeID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns>danh sách tài sản và tổng số bản ghi</returns>
        /// NTD 22/8/2022
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PagingData<Property>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPaging(
            [FromQuery] String? keyword,
            [FromQuery] Guid? departmentID,
            [FromQuery] Guid? propertyTypeID,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1)
        {
            try
            {
                // Kết nối DB
                string connectionString = "Server=localhost;Port=3307;Database=misa.web07.hcsn2.duong;Uid=root;Pwd=yp2382001;";
                var mySqlConnection = new MySqlConnection(connectionString);

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

                if (propertyTypeID != null)
                {
                    andConditions.Add($"PropertyTypeID LIKE '%{propertyTypeID}%'");
                }
                if (departmentID != null)
                {
                    andConditions.Add($"DepartmentID LIKE '%{departmentID}%'");
                }

                if (andConditions.Count > 0)
                {
                    whereClause += $" AND {string.Join(" AND ", andConditions)}";
                }

                parameters.Add("v_Where", whereClause);

                // Chạy store procedure
                var multipleResults = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                
                if (multipleResults != null)
                {
                    var properties = multipleResults.Read<Property>().ToList();
                    var totalCount = multipleResults.Read<long>().Single();
                    return StatusCode(StatusCodes.Status200OK, new PagingData<Property>()
                    {
                        Data = properties,
                        TotalCount = totalCount
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        /// <summary>
        /// lấy new code
        /// </summary>
        /// <returns></returns>
        /// NTD 22/8/2022
        [HttpGet("new-code")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(String))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult getNewPropertyCode()
        {
            try
            {
                // Kết nối DB
                String connectionString = "Server=localhost;Port=3307;Database=misa.web07.hcsn2.duong;Uid=root;Pwd=yp2382001;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //tạo câu lệnh
                String getMaxPropertyCodeCommand = "SELECT PropertyCode FROM property ORDER BY PropertyCode DESC LIMIT 1;";

                // chạy câu lệnh 
                var maxPropertyCode = mySqlConnection.QueryFirstOrDefault<string>(getMaxPropertyCodeCommand);

                var newPropertyCode = "TS" + (Int64.Parse(maxPropertyCode.Substring(2)) + 1).ToString();
                return StatusCode(StatusCodes.Status200OK, newPropertyCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }

        }
    }
}
