using TimeasyAPI.src.Models.ValueObjects.Enums;

namespace TimeasyCore.src.Models
{
    public class Subject 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CourseId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TimetableId { get; set; }
        public Guid RoomTypeId { get; set; }
        public bool IsDivided { get; set; } = false;
        public int DividedCount { get; set; }
        public SubjectComplexity Complexity { get; set; }
        public int WeeklyClassCount { get; set; }
        public int Period { get; set; }
        public int StudentsCount { get; set; }
    }
}
