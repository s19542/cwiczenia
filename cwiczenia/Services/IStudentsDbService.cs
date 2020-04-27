using cw.Models;
using cwiczenia.DTOs.Request;
using cwiczenia.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia.Services
{
    public interface IStudentsDbService
    {
        public IEnumerable<Student> GetStudents();

        public EnrollmentResponse EnrollStudent(EnrollStudentRequest enrollStudentRequest, string connectionString);
    }
}
