using TimeasyCore.src.Core;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Constraints;
using TimeasyScheduler.src.Core;
using TimeasyScheduler.src.Models;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public class InstituteIntervalConflictValidator : IValidator
    {

        public ValidationResult Validate(Schedule solution, CreateTimetableConfig timetable)
        {

            var result = new ValidationResult();
            var instituteIntervals = timetable.Institute.Intervals;

            foreach (var kvp in solution.ScheduleData)
            {
                List<TimeSlot> timeSlots = kvp.Value.Where( ts => ts.IsAllocated).ToList();

                foreach (var interval in instituteIntervals)
                {
                    TimeOnly intervalStart = TimeOnly.Parse(interval.Start);
                    TimeOnly intervalEnd = TimeOnly.Parse(interval.End);
                    foreach (var timeSlot in timeSlots)
                    {
                        TimeOnly timeSlotStart = timeSlot.StartTime;
                        TimeOnly timeSlotEnd = timeSlot.EndTime;

                        if (timeSlotStart <= intervalEnd && timeSlotEnd >= intervalStart)
                        {
                            result.IsValid = false;
                            result.FailedCount++;
                            result.TotalWeight += Constants.INSTITUTE_INTERVAL_CONFLICT_CONSTRAINT_WEIGHT;
                        }
                    }
                }
            }

            return result;
        }
    }
}
