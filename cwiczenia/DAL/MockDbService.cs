﻿using cw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        public MockDbService()
        {
            _students = new List<Student> { new Student{ IdStudent=1, FirstName="Jan", LastName="Kowalski"},
                new Student{ IdStudent=2, FirstName="Anna", LastName="Makewska"},
                new Student{ IdStudent=3, FirstName="Andrzej", LastName="And"}

            };
        }
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
