using cw.Models;
using System.Collections.Generic;


namespace cw.DAL
{
    // Data Access Layer
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
