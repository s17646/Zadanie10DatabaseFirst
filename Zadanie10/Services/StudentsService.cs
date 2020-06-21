using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadanie10.DTO;
using Zadanie10.Models;

namespace Zadanie10.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly StudentsDbContext _dbcontext;
        public StudentsService(StudentsDbContext context)
        {
            _dbcontext = context;
        }

        public void DeleteStudent(string indexNumber)
        {
            throw new NotImplementedException();
        }

        public Student GetStudent(string indexNumber)
        {
            throw new NotImplementedException();
        }

        public Student UpdateStudent(StudentDto studentDto)
        {
            throw new NotImplementedException();
        }
    }
}
