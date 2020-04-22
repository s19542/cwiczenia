using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw.DAL;
using cw.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw.Controllers
{
    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase
    {
       private readonly IDbService _dbService;
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public IActionResult GetStudent()
        {
            return Ok(_dbService.GetStudents());
        }
        [HttpGet("{id}/{grade}")]
        public IActionResult GetStudent(int id, string grade)
        {
            if (id == 1)
            {
                return Ok("Kowalski "+grade);

            }
            else if (id == 2)
            {
                return Ok("Malewski"+grade);

            }
            return NotFound("Nie znalieziono studenta"+grade);
        }


        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            //... add to database
            //... generating index number

            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
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