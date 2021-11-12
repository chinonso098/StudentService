using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentService.DataService;
using StudentService.DomainObjects;
using StudentService.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace StudentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StudentController : ControllerBase
    {
        private readonly StudentServiceContext _dbContext;

        public StudentController (StudentServiceContext injectedSSC) 
        {
            this._dbContext = injectedSSC;
        }


        // GET: api/Students
        [HttpGet]
        public IEnumerable<Student> GetStudentsAsync() 
        {
            return  _dbContext.Students.ToList();
        }
    
        //GET: api/Student
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> GetById(int id)
        {
            if (_dbContext.Students.Find(id) == null)
                return NotFound();
            else
             return _dbContext.Students.Find(id);
        }


        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateStudent(Student student)
        {
            student.StudentNumber = GeneralFunctions.GenerateStudentNumber();
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = student.StudentID }, student);
        }


        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateStudent(Student student) 
        {

            if (_dbContext.Students.Find(student.StudentID) == null)
                return NotFound();
            else
            {
                Student currentStudent = _dbContext.Students.Find(student.StudentID);

                currentStudent.FirstName = student.FirstName;
                currentStudent.LastName = student.LastName;
                currentStudent.StudentNumber = student.StudentNumber;
                currentStudent.Email = student.Email;
                currentStudent.DoB = student.DoB;
                currentStudent.AdmissionDate = student.AdmissionDate;
                currentStudent.Age = student.Age;

                _dbContext.Entry(currentStudent).State = EntityState.Modified;

                try
                {
                    _dbContext.Update(currentStudent);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    Console.WriteLine("e" + e.Message);

                    return BadRequest();
                }

                return NoContent();
            }
        }

    }
}
