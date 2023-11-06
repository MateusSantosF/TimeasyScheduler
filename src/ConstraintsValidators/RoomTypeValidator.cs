
using TimeasyCore.src.Core;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Constraints;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public class RoomTypeValidator : IValidator
    {
        private int ROOM_TYPE_CONSTRAINT_WEIGHT = 2;
  

        public ValidationResult Validate(Schedule solution, Timetable timetable)
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

                    bool hasValidType = room != null && subject != null && room.RoomTypeId == subject.RoomTypeId;

                    if (!hasValidType)
                    {
                        result.IsValid = false;
                        result.FailedCount++;
                        result.TotalWeight += ROOM_TYPE_CONSTRAINT_WEIGHT;
                    }
                }
            }

            return result;
        }
    }
}
