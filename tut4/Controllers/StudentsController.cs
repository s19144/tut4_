using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tut4.Models;
using tut4.Services;

namespace tut4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private string ConStr = "Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True";

        private IStudentsDb _dbService;

        public StudentsController(IStudentsDb db)
        {
            _dbService = db;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var result = new List<Student>();

            using (SqlConnection con = new SqlConnection(ConStr))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from students";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    result.Add(st);
                }

                return Ok(result);
            }
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            using (SqlConnection con = new SqlConnection(ConStr))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from students where indexNumber=@index";

                SqlParameter par1 = new SqlParameter();
                par1.ParameterName = "index";
                par1.Value = indexNumber;

                com.Parameters.Add(par1);
                //com.Parameters.AddWithValue("index", indexNumber);

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    return Ok(st);
                }

                return Ok();
            }
        }

        [HttpPost]
        public IActionResult CreateStudent([FromServices]IStudentsDb service, Student newStudent)
        {
            using (SqlConnection con = new SqlConnection(ConStr))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "insert into students(indexNumber, firstName, lastName) values (@par1, @par2, @par3)";

                com.Parameters.AddWithValue("par1", newStudent.IndexNumber);
                com.Parameters.AddWithValue("par2", newStudent.FirstName);
                com.Parameters.AddWithValue("par3", newStudent.LastName);

                con.Open();
                int affectedRows = com.ExecuteNonQuery();
            }

            return Ok();
        }

        [HttpGet("procedure")]
        public IActionResult GetStudents2()
        {
            using (SqlConnection con = new SqlConnection(ConStr))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "TestProc2";
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("LastName", "Kowalski");

                var dr = com.ExecuteReader();
                
            }

            return Ok();
        }

        [HttpGet("procedure2")]
        public IActionResult GetStudents3()
        {
            using (SqlConnection con = new SqlConnection(ConStr))
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "insert";
                com.Connection = con;

                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    com.ExecuteNonQuery(); 

                    com.CommandText = "update ";
                    com.ExecuteNonQuery();
                    tran.Commit();
                }
                catch (Exception exc)
                {
                    tran.Rollback();
                }

            }

            return Ok();
        }
    }
}