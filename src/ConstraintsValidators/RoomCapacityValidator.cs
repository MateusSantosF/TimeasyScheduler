
using FluentValidation;
using TimeasyCore.src.Core;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Constraints;
using TimeasyScheduler.src.Core;
using TimeasyScheduler.src.Models;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public class RoomCapacityValidator : IValidator
    {
  
        public ValidationResult Validate(Schedule solution, CreateTimetableConfig timetable)
        {
            var result = new ValidationResult();

            foreach (var kvp in solution.ScheduleData)
            {
                List<TimeSlot> timeSlots = kvp.Value.Where(ts => ts.IsAllocated).ToList();

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
                        result.FailedCount++;
                        result.TotalWeight += Constants.ROOM_CAPACITY_CONSTRAINT_WEIGHT;
                    }
                }
            }

            return result;
        }
    }
}
