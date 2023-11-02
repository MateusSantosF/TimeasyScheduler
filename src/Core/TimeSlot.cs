namespace TimeasyCore.src.Core
{
    public class TimeSlot
    {
        public string Name { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid TeacherID { get; set; }
        public Guid SubjectID { get; set; }
        public Guid CourseID { get; set; }
        public Guid RoomID { get; set; }
    }
}