using cw.Models;
using cwiczenia.DTOs.Request;
using cwiczenia.DTOs.Response;
using cwiczenia.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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


        [HttpGet]
        public IActionResult Get()//коли немає цього методу викидує 405 
        {
            return Ok("Okejczyk");
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            //mapowanie obiektu reguest 
            //business models
            //mapowanie obiektu response
            Console.WriteLine("37");
            var enrollStudentRequest = new EnrollStudentRequest();
            enrollStudentRequest.IndexNumber = request.IndexNumber;
            enrollStudentRequest.FirstName = request.FirstName;
            enrollStudentRequest.LastName = request.LastName;
            enrollStudentRequest.BirthDate = (System.DateTime)request.BirthDate;
            enrollStudentRequest.Semester = request.Semester;
            enrollStudentRequest.Studies = request.Studies;

           
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {

               
                com.Connection = con;
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                com.Transaction = transaction;


               
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
                    dr.Close();
                    //Does enrollment exist?
                    com.CommandText = "select IdEnrollment from Enrollment where semester=1 and IdStudy=@idStudies and StartDate>=CONVERT(DATE, GETDATE());";
                    com.Parameters.AddWithValue("idStudies", idStudies);
                     
                    var  dr11 = com.ExecuteReader();  
                    
                    int idEnrollmet;
                    if (!dr11.Read())
                    {
                        
                        com.CommandText = " insert into enrollment (IdEnrollment, Semester, IdStudy, StartDate) values ((select MAX(IdEnrollment)+1 from enrollment), 1, @idSt,CONVERT(DATE, GETDATE()));";
                        com.Parameters.AddWithValue("idSt", idStudies);
                         dr11.Close();
                           com.ExecuteNonQuery();
                        
                        
                        com.CommandText = "select b.IdEnrollment from (select * from Enrollment where semester=1 and IdStudy=@idS) b where StartDate=(select MAX(a.StartDate) from (select * from Enrollment where semester=1 and IdStudy=@idS)a);";
                        com.Parameters.AddWithValue("idS", idStudies);
                          
                        var  dr1 = com.ExecuteReader();
                        idEnrollmet = (int)dr1["IdEnrollment"];return Ok("92");
                        dr1.Close();
                            return Ok("oooooooooooooooo");
                    }
                    else
                    {
                        idEnrollmet = (int)dr11["IdEnrollment"];
                        dr11.Close();
                    }
                    
                    //Is the indexNumber of student unique?

                    com.CommandText = "select * from Student where IndexNumber like @id;";
                    com.Parameters.AddWithValue("id", enrollStudentRequest.IndexNumber);
                    
                    var dr4 = com.ExecuteReader();
                    if (dr4.Read())
                    {
                        dr4.Close();
                        transaction.Rollback();
                        return BadRequest("Index number is not unique !!!");

                    }
                   

                    //Add new student
                    com.CommandText = "insert into student (IndexNumber, FirstName,LastName, BirthDate, IdEnrollment) values (@IndexNumber, @FirstName,@LastName,@BirthDate,@IdEnrollment);";

                    com.Parameters.AddWithValue("IndexNumber", enrollStudentRequest.IndexNumber);
                    com.Parameters.AddWithValue("FirstName", enrollStudentRequest.FirstName);
                    com.Parameters.AddWithValue("LastName", enrollStudentRequest.LastName);
                    com.Parameters.AddWithValue("BirthDate", enrollStudentRequest.BirthDate);
                    com.Parameters.AddWithValue("IdEnrollment", idEnrollmet);
                    if (!dr4.IsClosed)
                    {
                        dr4.Close();
                    }
                    com.ExecuteNonQuery();

                    //return Ok("92");
                    transaction.Commit();

                    var response = new EnrollmentResponse();
                    response.Semester = "1";
                    return Created("WWWWWWEEEEEEEEEEEEELLLLLLLLLLL DOOOOOOOOOONE", response);

                }
                catch (SqlException ex)
                {
                    transaction.Rollback();

                }

            }

            return BadRequest("SUUUUUUUUUUUUUUUUUUKAAAAAAAAAAAAAAAAAAAA");
        }

    }
}