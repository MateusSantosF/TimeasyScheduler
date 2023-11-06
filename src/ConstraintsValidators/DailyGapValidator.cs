
using TimeasyCore.src.Core;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Constraints;
using TimeasyScheduler.src.Core;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public class DailyGapValidator : IValidator
    {
        public ValidationResult Validate(Schedule solution, Timetable timetable)
        {
            var result = new ValidationResult();

            foreach (DayOfWeek dayOfWeek in solution.ScheduleData.Keys)
            {
                if (solution.ScheduleData.TryGetValue(dayOfWeek, out List<TimeSlot> timeSlots))
                {
                    DayOfWeek previousDay = GetPreviousDay(dayOfWeek);
                    if (solution.ScheduleData.ContainsKey(previousDay))
                    {
                        var lastTimeSlot = solution.ScheduleData[previousDay].Last();
                        var firstTimeSlot = timeSlots.First();

                        TimeOnly lastClassEndTime = lastTimeSlot.EndTime;
                        TimeOnly firstClassStartTime = firstTimeSlot.StartTime;

                        TimeSpan timeGap = firstClassStartTime - lastClassEndTime;

                        if (timeGap.TotalHours < Constants.DAILY_GAP_MIN_TIME_IN_HOURS)
                        {
                            result.IsValid = false;
                            result.FailedCount++;
                            result.TotalWeight += Constants.DAILY_GAP_CONSTRAINT_WEIGHT;
                        }
                    }
                }
            }

            return result;
        }

        private DayOfWeek GetPreviousDay(DayOfWeek currentDay)
        {
            int previousDayValue = ((int)currentDay - 1 + 6) % 6;
            return (DayOfWeek)previousDayValue;
        }

    }
}
