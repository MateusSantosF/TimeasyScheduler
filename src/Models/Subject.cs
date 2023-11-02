using TimeasyAPI.src.Models.ValueObjects.Enums;
using TimeasyScheduler.src.Core.Interfaces;

namespace TimeasyCore.src.Models
{
    public class Subject 
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CourseId { get; set; }

        public Guid RoomTypeId { get; set; }

        public SubjectComplexity Complexity { get; set; }

        public int WeeklyClassCount { get; set; }

        public int StudentsCount { get; set; }

    }
}
