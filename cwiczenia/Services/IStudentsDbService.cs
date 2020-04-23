using cw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia.Services
{
    interface IStudentsDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
