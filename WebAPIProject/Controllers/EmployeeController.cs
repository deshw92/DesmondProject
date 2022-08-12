using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAPIProject.Models;

namespace WebAPIProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string dataSource = _configuration.GetConnectionString("desmonddbconn");
            string query = "SELECT a.Id,a.EmployeeName,a.DepartmentId,a.PhotoFileName,a.CreatedBy,DATE_FORMAT(a.CreatedOn,'%Y-%m-%d %H:%i:%s') as 'CreatedOn',a.LastUpdatedBy,DATE_FORMAT(a.LastUpdatedOn,'%Y-%m-%d %H:%i:%s') as 'LastUpdatedOn',a.Deleted,b.DepartmentName FROM `Employee` a INNER JOIN `Department` b ON a.`DepartmentId` = b.Id WHERE a.`Deleted` = 0 AND b.`Deleted` = 0";
            DataTable table = new DataTable();
            MySql.Data.MySqlClient.MySqlDataReader dataReader;
            using (MySql.Data.MySqlClient.MySqlConnection _conn = new MySql.Data.MySqlClient.MySqlConnection(dataSource))
            {
                _conn.Open();
                using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, _conn))
                {
                    dataReader = command.ExecuteReader();
                    table.Load(dataReader);
                    _conn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee employee)
        {
            try
            {
                string dataSource = _configuration.GetConnectionString("desmonddbconn");
                string query = "INSERT INTO `Employee`(`EmployeeName`,`DepartmentId`,`PhotoFileName`,`CreatedBy`,`CreatedOn`,`LastUpdatedBy`,`LastUpdatedOn`,`Deleted`) VALUES (@employeeName, @departmentId, @photoFileName, @user, CURRENT_TIMESTAMP, @user, CURRENT_TIMESTAMP, 0)";
                using (MySql.Data.MySqlClient.MySqlConnection _conn = new MySql.Data.MySqlClient.MySqlConnection(dataSource))
                {
                    _conn.Open();
                    using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, _conn))
                    {
                        command.Parameters.AddWithValue("@employeeName", employee.EmployeeName);
                        command.Parameters.AddWithValue("@departmentId", employee.Department?.Id);
                        command.Parameters.AddWithValue("@photoFileName", employee.PhotoFileName);
                        command.Parameters.AddWithValue("@user", "Desmond");
                        int result = command.ExecuteNonQuery();
                        _conn.Close();
                    }
                }
                return new JsonResult("Record added successfully.");
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }

        [HttpPut]
        public JsonResult Put(Employee employee)
        {
            try
            {
                string dataSource = _configuration.GetConnectionString("desmonddbconn");
                string query = @"UPDATE Employee 
                    SET `EmployeeName` = @employeeName, 
                    `DepartmentId` = @departmentId, 
                    `PhotoFileName` = @photoFileName, 
                    `LastUpdatedBy` = @user, 
                    `LastUpdatedOn` = CURRENT_TIMESTAMP
                    WHERE Id = @Id";
                using (MySql.Data.MySqlClient.MySqlConnection _conn = new MySql.Data.MySqlClient.MySqlConnection(dataSource))
                {
                    _conn.Open();
                    using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, _conn))
                    {
                        command.Parameters.AddWithValue("@employeeName", employee.EmployeeName);
                        command.Parameters.AddWithValue("@departmentId", employee.Department?.Id);
                        command.Parameters.AddWithValue("@photoFileName", employee.PhotoFileName);
                        command.Parameters.AddWithValue("@user", "Desmond");
                        command.Parameters.AddWithValue("@Id", employee.Id);
                        int result = command.ExecuteNonQuery();
                        _conn.Close();
                    }
                }
                return new JsonResult("Record updated successfully.");
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }

        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {
            try
            {
                string dataSource = _configuration.GetConnectionString("desmonddbconn");
                string query = @"UPDATE Employee 
                    SET `Deleted` = 1, 
                    `LastUpdatedBy` = @user, 
                    `LastUpdatedOn` = CURRENT_TIMESTAMP
                    WHERE Id = @Id";
                using (MySql.Data.MySqlClient.MySqlConnection _conn = new MySql.Data.MySqlClient.MySqlConnection(dataSource))
                {
                    _conn.Open();
                    using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, _conn))
                    {
                        command.Parameters.AddWithValue("@user", "Desmond");
                        command.Parameters.AddWithValue("@Id", Id);
                        int result = command.ExecuteNonQuery();
                        _conn.Close();
                    }
                }
                return new JsonResult("Record deleted successfully.");
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var request = Request.Form;
                var uploadFile = request.Files[0];
                string fileName = uploadFile.FileName;
                string physicalPath = _env.ContentRootPath + "/Photos/" + fileName;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    uploadFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch (Exception e)
            {
                return new JsonResult(e);
            }
        }
    }
}
