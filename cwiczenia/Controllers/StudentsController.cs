using System.Collections.Generic;
using System.Data.SqlClient;
using cw.DAL;
using cw.Models;
using Microsoft.AspNetCore.Mvc;


namespace cw.Controllers
{
    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase
    {

        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19542;Integrated Security=True";
        private readonly IDbService _dbService;
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudent()
        {
            var list = new List<Student>();

            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select FirstName,LastName,BirthDate, Name, Semester from Student INNER JOIN Enrollment en ON Student.IdEnrollment = en.IdEnrollment INNER JOIN Studies st ON en.IdStudy = st.IdStudy;";

                con.Open();

                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var student = new Student();
                    student.FirstName = dr["FirstName"].ToString();
                    student.LastName = dr["LastName"].ToString();
                    student.BirthDate = dr["BirthDate"].ToString();
                    student.Studies = dr["Name"].ToString();
                    student.Semester = dr["Semester"].ToString();

                    list.Add(student);

                }

            }

            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select Semester, StartDate, Name  from Enrollment INNER JOIN Studies on Enrollment.IdStudy = Studies.IdStudy WHERE IdEnrollment = (select IdEnrollment from student where indexNumber like @id ); ";
                com.Parameters.AddWithValue("id", id);

                con.Open();
                var dr = com.ExecuteReader();

                while (dr.Read()) { 

                   return Ok(string.Concat("Semester: " + dr["Semester"].ToString(), "\nStartDate: ", dr["StartDate"].ToString(), "\nName of studies: ", dr["Name"].ToString()));
                }



                 return NotFound("Nie znalieziono studenta");
            }
        }


        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            //... add to database
            //... generating index number

            //  student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            // _dbService.GetStudents().Add(student);//зберігає до нашої мокдб, але після перезапуску програми запосчені дані зникають            
            //коли метод вертає ICollection<Student>
            return Ok(student);//вертає в джейсоні але сереалізатор можна змінити за допомогою мідла в стартапі 
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id)
        {
            return Ok("Aktualizacja dokończona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {

            return Ok("Usuwanie ukończone");
        }



    }
}