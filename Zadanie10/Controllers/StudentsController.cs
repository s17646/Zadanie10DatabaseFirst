using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadanie10.DTO;
using Zadanie10.Services;

namespace Zadanie10.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _service;

        public StudentsController(IStudentsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            try
            {
                return Ok(_service.GetStudent(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_service.GetStudents());
        }


        [HttpPut]
        public IActionResult UpdateStudent(StudentDto studentDto)
        {
            var studentId = studentDto.IndexNumber;
            try
            {
                _service.UpdateStudent(studentDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Student updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(string id)
        {
            try
            {
                _service.DeleteStudent(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Student deleted");
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentDtoRequest request)
        {
            try
            {
                _service.EnrollStudent(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Student enrolled");
        }


        [HttpPost]
        [Route("promotions")]
        public IActionResult PromoteStudents(int semester, string studies) 
        {
            try
            {
                _service.PromoteStudents(semester, studies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("All students promoted!");
        }

    }
}
