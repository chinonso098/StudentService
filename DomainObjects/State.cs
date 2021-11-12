using System.ComponentModel.DataAnnotations;

namespace StudentService.DomainObjects
{
    public class State
    {
        [Key]
        public int StateID { get; set; }

        public string Name { get; set; }
    }
}