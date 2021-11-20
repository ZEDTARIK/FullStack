using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ApiNetCore.Models;

namespace ApiNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        // Depandency Injection 
        private readonly IConfiguration _configuration;

         public DepartmentController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        // GET Department 

        [HttpGet]
        public ActionResult Get()
        {
            string query = "SELECT DepartmentId, DepartmentName FROM dbo.Department";

            DataTable table = new DataTable();
            SqlDataReader myReader;

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public ActionResult Post(Department dep)
        {
            string query = @"INSERT INTO dbo.Department 
                             VALUES (@DepartmentName)";

            DataTable table = new DataTable();
            SqlDataReader myReader;

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("New Department Inserted With SuccessFully");
        }

        [HttpPut]
        public ActionResult Put(Department dep)
        {
            string query = @"UPDATE dbo.Department 
                             SET DepartmentName = @DepartmentName
                            WHERE DepartmentId = @DepartmentId";

            DataTable table = new DataTable();
            SqlDataReader myReader;

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Update With SuccessFully");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            string query = @"DELETE FROM  dbo.Department 
                            WHERE DepartmentId = @DepartmentId";

            DataTable table = new DataTable();
            SqlDataReader myReader;

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted With SuccessFully");
        }

    }
}
