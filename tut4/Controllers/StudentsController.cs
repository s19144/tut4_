using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tut4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private string ConStr = "Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True";

        [HttpGet]
        public IActionResult GetStudents()
        {

            using (SqlConnection con = new SqlConnection(ConStr))
            using (SqlCommand com=new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "Select * from Student";
                

                con.Open();     
                

            }

            return Ok();
        }
    }
}