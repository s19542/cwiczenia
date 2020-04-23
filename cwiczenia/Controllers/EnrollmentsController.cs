using cw.Models;
using cwiczenia.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace cwiczenia.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request) { 

            //mapowanie obiektu reguest 
            //business models
            //mapowanie obiektu response

            var enrollStudentRequest = new EnrollStudentRequest();
            enrollStudentRequest.IndexNumber=  request.IndexNumber;
            enrollStudentRequest.FirstName = request.FirstName;
            enrollStudentRequest.LastName = request.LastName;
            enrollStudentRequest.BirthDate = request.BirthDate;
            enrollStudentRequest.Semester = request.Semester;
            enrollStudentRequest.Studies = request.Studies;




            return Ok();

            return BadRequest();//400
        }

    }
}