using System;
using System.Collections.Generic;
using System.Linq;
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

        public void EnrollStudent(EnrollStudentDtoRequest request)
        {
            var studiesFound = _dbcontext.Studies.Where(s => s.Name.Equals(request.Name)).FirstOrDefault();
            if (studiesFound == null)
            {
                throw new Exception("No studies with given name");
            }
            var idStudy = studiesFound.IdStudy;

            var studentWithNewIndexNumber = _dbcontext.Student.Where(s => s.IndexNumber.Equals(request.IndexNumber)).FirstOrDefault();
            if (studentWithNewIndexNumber != null)
            {
                throw new Exception("Student already exists");
            }

            var maxIdEnrollment = _dbcontext.Enrollment.Max(e => e.IdEnrollment);
            var nextIdEnrollment = maxIdEnrollment++;

            var newEnrollemnt = new Enrollment();
            newEnrollemnt.IdEnrollment = nextIdEnrollment;
            newEnrollemnt.Semester = 1;
            newEnrollemnt.IdStudy = idStudy;
            newEnrollemnt.StartDate = DateTime.Now;
            _dbcontext.Add(newEnrollemnt);
            _dbcontext.SaveChanges();
            var newStudent = new Student();
            newStudent.IndexNumber = request.IndexNumber;
            newStudent.FirstName = request.FirstName;
            newStudent.LastName = request.LastName;
            newStudent.BirthDate = request.BirthDate;
            newStudent.IdEnrollment = nextIdEnrollment;
            _dbcontext.Add(newStudent);
            _dbcontext.SaveChanges();

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

        public void PromoteStudents(int semester, string studies)
        {
            var studiesFound = _dbcontext.Studies.Where(s => s.Name.Equals(studies)).FirstOrDefault();
            if (studiesFound == null)
            {
                throw new Exception("Studies don't exist");
            }
            var idStudy = studiesFound.IdStudy;
            var students = _dbcontext.Enrollment.Where(s => s.Semester.Equals(semester) && s.IdStudy.Equals(idStudy)).ToList();
            students.ForEach(s => s.Semester = semester + 1);
            _dbcontext.SaveChanges();
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
