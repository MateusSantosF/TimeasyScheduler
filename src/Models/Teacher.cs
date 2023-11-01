namespace TimeasyCore.src.Models
{
    public class Teacher
    {

        public Guid Id { get; set; }

        public List<Interval> WorkTime { get; set; }

        public List<Subject> PreferredSubjects { get; set; }
    }
}
