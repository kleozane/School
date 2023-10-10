using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }


        [ForeignKey("CurriculumId")]
        public int? CurriculumId { get; set; }
        public Curriculum Curriculum { get; set; }
    }
}
