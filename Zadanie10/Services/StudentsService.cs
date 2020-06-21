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
            var student = (Student)_dbcontext.Student.Where(st => st.IndexNumber == indexNumber);
            if (student == null)
            {
                throw new Exception("Student doesn't exist");
            }

            _dbcontext.Remove(student);
        }

        public Student GetStudent(string indexNumber)
        {
            var student = _dbcontext.Student.Where(st => st.IndexNumber == indexNumber);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            return null;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _dbcontext.Student.ToList();
        }

        public Student UpdateStudent(StudentDto studentDto)
        {
            var student = (Student)_dbcontext.Student.Where(st => st.IndexNumber == studentDto.IndexNumber);
            if (student == null)
            {
                throw new Exception("Student doesn't exist");
            }

            if (!String.IsNullOrEmpty(studentDto.FirstName))
            {
                student.FirstName = studentDto.FirstName;
            }
            if (!String.IsNullOrEmpty(studentDto.LastName))
            {
                student.LastName = studentDto.LastName;
            }
            if (studentDto.BirthDate != null)
            {
                student.BirthDate = studentDto.BirthDate;
            }
            _dbcontext.SaveChanges();
            return student;
        }
    }
}
