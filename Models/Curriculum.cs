namespace School.Models
{
    public class Curriculum
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short Weeks { get; set; }
        public short Hours { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}
