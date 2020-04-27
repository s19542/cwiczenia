using cw.Models;
using cwiczenia.DTOs.Request;
using cwiczenia.DTOs.Response;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace cwiczenia.Services
{
    public class SqlServerDbDal : IStudentsDbService
    {
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest enrollStudentRequest, string connectionString)
        {
            using (var con = new SqlConnection(connectionString))
            using (var com = new SqlCommand())
            {
                //Does exist this study?
                com.CommandText = "select IdStudy from studies where name like @studies ;";
                com.Parameters.AddWithValue("studies", enrollStudentRequest.Studies);

                var dr = com.ExecuteReader();

                if (!dr.Read())
                {
                    return null;
                }









            }

            return null;
        }

        public IEnumerable<Student> GetStudents()
        {
            return null;
        }

    }
}
