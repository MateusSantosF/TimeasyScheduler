
using TimeasyCore.src.Core;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Constraints;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public class ScheduleConflictValidator : IValidator
    {
        private int SCHEDULE_CONFLICT_CONSTRAINT_WEIGHT = 2;

        public ValidationResult Validate(Schedule solution, Timetable timetable)
        {
            var result = new ValidationResult();

            foreach (var kvp in solution.ScheduleData)
            {
                List<TimeSlot> timeSlots = kvp.Value;
                timeSlots.Sort((t1, t2) => t1.StartTime.CompareTo(t2.StartTime));

                for (int i = 1; i < timeSlots.Count; i++)
                {
                    if (timeSlots[i].StartTime <= timeSlots[i - 1].EndTime)
                    {
                        result.IsValid = false;
                        result.FailedCount++;
                        result.TotalWeight += SCHEDULE_CONFLICT_CONSTRAINT_WEIGHT;
                    }
                }
            }

            return result;
        }
    }
}
