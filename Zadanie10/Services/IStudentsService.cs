using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadanie10.DTO;
using Zadanie10.Models;

namespace Zadanie10.Services
{
    public interface IStudentsService
    {
        Student GetStudent(string indexNumber);
        Student UpdateStudent(StudentDto studentDto);
        void DeleteStudent(string indexNumber);
        IEnumerable<Student> GetStudents();
    }
}
