namespace TimeasyCore.src.Core
{
    public class TimeSlot
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Name { get; set; }
        public Guid TeacherID { get; set; }
        public Guid SubjectID { get; set; }
        public Guid CourseID { get; set; }
        public Guid RoomID { get; set; }
    }
}