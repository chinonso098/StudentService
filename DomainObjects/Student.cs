using System;
using System.ComponentModel.DataAnnotations;

namespace StudentService.DomainObjects
{
    public class Student
    {
        [Key]
        public int StudentID {get; set;}

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string StudentNumber { get; set; }

        public string Email {get; set;}

        public DateTime DoB { get; set; }

        public DateTime AdmissionDate { get; set; }
        
        public string Program { get; set; }
    }
}
