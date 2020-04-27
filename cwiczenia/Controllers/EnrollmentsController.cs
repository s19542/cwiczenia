using cw.Models;
using cwiczenia.DTOs.Request;
using cwiczenia.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace cwiczenia.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19542;Integrated Security=True";
        private readonly IStudentsDbService service;

        public EnrollmentsController(IStudentsDbService studentsDbService)
        {
            service = studentsDbService;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            //mapowanie obiektu reguest 
            //business models
            //mapowanie obiektu response

            var enrollStudentRequest = new EnrollStudentRequest();
            enrollStudentRequest.IndexNumber = request.IndexNumber;
            enrollStudentRequest.FirstName = request.FirstName;
            enrollStudentRequest.LastName = request.LastName;
            enrollStudentRequest.BirthDate = request.BirthDate;
            enrollStudentRequest.Semester = request.Semester;
            enrollStudentRequest.Studies = request.Studies;


            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                var tran = con.BeginTransaction();
                //Does exist this study?
                try
                {
                    com.CommandText = "select IdStudy from studies where name like @studies ;";
                    com.Parameters.AddWithValue("studies", enrollStudentRequest.Studies);

                    var dr = com.ExecuteReader();

                    if (!dr.Read())
                    {
                        return BadRequest("Studia nie istnieja");
                    }

                    int idStudies = (int)dr["IdStudy"];


                    //Add new student
                    com.CommandText = "insert into student  (IndexNumber, FirstName,LastName, BirthDate, ...) values (@IndexNumber, @FirstName,@LastName,@BirthDate, ...)";
                    // com.Parameters.AddWithValue("");

                    com.ExecuteNonQuery();


                    tran.Commit();

                }
                catch (SqlException ex)
                {
                    tran.Rollback();

                }









            }



            return Ok();

            return BadRequest();//400
        }

    }
}