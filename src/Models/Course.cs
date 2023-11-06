using TimeasyAPI.src.Models.ValueObjects.Enums;

namespace TimeasyCore.src.Models
{
    public class Course
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Turn Turn { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}
