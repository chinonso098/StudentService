using System.ComponentModel.DataAnnotations;

namespace StudentService.DomainObjects
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }

        public int StudentID { get; set; }

        public Student Student { get; set;}

        public string StreetOne { get; set; }

        public string StreetTwo { get; set; }

        public string City { get; set; }

        public int StateID { get; set; }

        public State State { get; set; }

        public string ZipCode { get; set; }

        
    }
}
