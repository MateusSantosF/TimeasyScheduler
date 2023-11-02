namespace TimeasyCore.src.Models
{
    public class Timetable
    {
        public Guid Id { get; set; }

        public Institute Institute { get; set; }

        public List<Teacher> Teachers { get; set; } = new();

        public List<Room> Rooms { get; set; } = new();

        public List<Course> Courses { get; set; } = new();

    }
}
