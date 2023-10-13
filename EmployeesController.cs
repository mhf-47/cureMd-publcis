using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using WebApplication1;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly string _connectionString;

        public EmployeesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllEmployees", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
                        emp.ID = (int)reader["ID"];
                        emp.Name = reader["Name"].ToString();
                        emp.Age = (int)reader["Age"];
                        emp.DepartmentID = (int)reader["DepartmentID"];

                        employees.Add(emp);
                    }
                }
            }
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetEmployeeById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        employee.ID = (int)reader["ID"];
                        employee.Name = reader["Name"].ToString();
                        employee.Age = (int)reader["Age"];
                        employee.DepartmentID = (int)reader["DepartmentID"];
                    }
                }
            }
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult AddEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("AddEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Age", employee.Age);
                    command.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdateEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Age", employee.Age);
                    command.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DeleteEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return Ok();
        }
    }
}
