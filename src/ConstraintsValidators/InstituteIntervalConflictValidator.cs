using TimeasyCore.src.Core;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Constraints;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public class InstituteIntervalConflictValidator : IValidator
    {
        private int INSTITUTE_INTERVAL_CONFLICT_CONSTRAINT_WEIGHT = 2;

        public ValidationResult Validate(Schedule solution, Timetable timetable)
        {

            var result = new ValidationResult();
            var instituteIntervals = timetable.Institute.Intervals;

            foreach (var kvp in solution.ScheduleData)
            {
                List<TimeSlot> timeSlots = kvp.Value;

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
                            result.TotalWeight += INSTITUTE_INTERVAL_CONFLICT_CONSTRAINT_WEIGHT;
                        }
                    }
                }
            }

            return result;
        }
    }
}
