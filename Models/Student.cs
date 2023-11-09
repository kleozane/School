using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        [ForeignKey("CurriculumId")]
        public int? CurriculumId { get; set; }
        public Curriculum Curriculum { get; set; }
    }
}
