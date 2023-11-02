
using TimeasyCore.src.Core;
using TimeasyScheduler.src.Constraints;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public class RoomCapacityValidator : IValidator
    {
        private int ROOM_CAPACITY_CONSTRAINT_WEIGHT = 2;
  
        public ValidationResult Validate(Schedule solution, TimeasyCore.src.Models.Timetable timetable)
        {
            var result = new ValidationResult();

            foreach (var kvp in solution.ScheduleData)
            {
                List<TimeSlot> timeSlots = kvp.Value;

                foreach (var t in timeSlots)
                {
                    var room = timetable.Rooms.Find(r => r.Id.Equals(t.RoomID));
                    var subject = timetable.Courses
                                 .Where(c => c.Id == t.CourseID)
                                 .SelectMany(c => c.Subjects)
                                 .FirstOrDefault(subject => subject.Id == t.SubjectID);

                    bool hasCapacity = room != null && subject != null && room.Capacity >= subject.StudentsCount;

                    if (!hasCapacity)
                    {
                        result.IsValid = false;
                        result.TotalWeight += ROOM_CAPACITY_CONSTRAINT_WEIGHT;
                    }
                }
            }

            return result;
        }
    }
}
