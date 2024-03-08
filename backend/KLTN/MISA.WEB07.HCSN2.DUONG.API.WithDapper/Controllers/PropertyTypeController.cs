﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB07.HCSN2.DUONG.API.WithDapper.Entity;

using MySqlConnector;
using Swashbuckle.AspNetCore.Annotations;

namespace MISA.WEB07.HCSN2.DUONG.API.WithDapper.API.WithDapper.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertyTypesController : ControllerBase
    {
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(List<PropertyType>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllPropertyType()
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3307;Database=misa.web07.hcsn2.duong;Uid=root;Pwd=yp2382001;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh truy vấn
                string getAllPropertyTypeCommand = "SELECT * FROM propertytype;";

                // Thực hiện gọi vào DB để chạy câu lệnh truy vấn ở trên
                var departments = mySqlConnection.Query<PropertyType>(getAllPropertyTypeCommand);

                // Xử lý dữ liệu trả về
                if (departments != null)
                {
                    // Trả về dữ liệu cho client
                    return StatusCode(StatusCodes.Status200OK, departments);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }
    }
}
