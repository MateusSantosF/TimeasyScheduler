
using TimeasyCore.src.Core;
using TimeasyScheduler.src.Constraints;
using TimeasyScheduler.src.Core;
using TimeasyScheduler.src.Models;

namespace TimeasyScheduler.src.ConstraintsValidators
{
    public class DailyGapValidator : IValidator
    {
        public ValidationResult Validate(Schedule solution, CreateTimetableConfig timetable)
        {
            var result = new ValidationResult();

            foreach (DayOfWeek dayOfWeek in solution.ScheduleData.Keys)
            {
                if (solution.ScheduleData.TryGetValue(dayOfWeek, out List<TimeSlot>? lastDayTimeSlots))
                {
                    if (lastDayTimeSlots is null) continue;

                    DayOfWeek previousDay = GetPreviousDay(dayOfWeek);
                    if (solution.ScheduleData.ContainsKey(previousDay))
                    {
                        var nextDayTimeSlots = solution.ScheduleData[previousDay];
                        lastDayTimeSlots.Sort();
                        nextDayTimeSlots.Sort();

                        var firstTimeSlot = lastDayTimeSlots.FirstOrDefault(ls => ls.IsAllocated);
                        var lastTimeSlot = nextDayTimeSlots.LastOrDefault(ls => ls.IsAllocated);

                        if(firstTimeSlot is null || lastTimeSlot is null) continue;

                            
                        TimeOnly firstClassStartTime = firstTimeSlot.StartTime;
                        TimeOnly lastClassEndTime = lastTimeSlot.EndTime;

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
