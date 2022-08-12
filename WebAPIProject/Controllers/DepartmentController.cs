using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAPIProject.Models;

namespace WebAPIProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string dataSource = _configuration.GetConnectionString("desmonddbconn");
            string query = "SELECT Id,DepartmentName,CreatedBy,DATE_FORMAT(CreatedOn,'%Y-%m-%d %H:%i:%s') as 'CreatedOn',LastUpdatedBy,DATE_FORMAT(LastUpdatedOn,'%Y-%m-%d %H:%i:%s') as 'LastUpdatedOn',Deleted FROM Department WHERE `Deleted` = 0";
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
        public JsonResult Post(Department department)
        {
            try
            {
                string dataSource = _configuration.GetConnectionString("desmonddbconn");
                string query = "INSERT INTO `Department`(`DepartmentName`,`CreatedBy`,`CreatedOn`,`LastUpdatedBy`,`LastUpdatedOn`,`Deleted`) VALUES (@departmentName, @user, CURRENT_TIMESTAMP, @user, CURRENT_TIMESTAMP, 0)";
                using (MySql.Data.MySqlClient.MySqlConnection _conn = new MySql.Data.MySqlClient.MySqlConnection(dataSource))
                {
                    _conn.Open();
                    using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, _conn))
                    {
                        command.Parameters.AddWithValue("@departmentName", department.DepartmentName);
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
        public JsonResult Put(Department department)
        {
            try
            {
                string dataSource = _configuration.GetConnectionString("desmonddbconn");
                string query = @"UPDATE Department 
                    SET `DepartmentName` = @departmentName, 
                    `LastUpdatedBy` = @user, 
                    `LastUpdatedOn` = CURRENT_TIMESTAMP
                    WHERE `Id` = @Id AND `Deleted` = 0";
                using (MySql.Data.MySqlClient.MySqlConnection _conn = new MySql.Data.MySqlClient.MySqlConnection(dataSource))
                {
                    _conn.Open();
                    using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query, _conn))
                    {
                        command.Parameters.AddWithValue("@departmentName", department.DepartmentName);
                        command.Parameters.AddWithValue("@user", "Desmond");
                        command.Parameters.AddWithValue("@Id", department.Id);
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
                    WHERE `DepartmentId` = @Id";
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

                string query2 = @"UPDATE Department 
                    SET `Deleted` = 1, 
                    `LastUpdatedBy` = @user, 
                    `LastUpdatedOn` = CURRENT_TIMESTAMP
                    WHERE `Id` = @Id";
                using (MySql.Data.MySqlClient.MySqlConnection _conn = new MySql.Data.MySqlClient.MySqlConnection(dataSource))
                {
                    _conn.Open();
                    using (MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(query2, _conn))
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
    }
}
