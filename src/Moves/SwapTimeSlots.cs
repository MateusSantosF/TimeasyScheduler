using TimeasyCore.src.Core;
using TimeasyScheduler.src.Core;

namespace TimeasyScheduler.src.Moves
{
    public class SwapTimeSlots
    {
        public static void Execute(ref Schedule solution, TabuList<SwapTimeSlotMove> tabuList, int currentBestWeight)
        {
            Random random = new Random();
            bool isTabu = true;

            while (isTabu)
            {
                DayOfWeek randomDay = solution.ScheduleData.Keys.ElementAt(random.Next(solution.ScheduleData.Count));

                if (solution.ScheduleData.ContainsKey(randomDay))
                {
                    var timeSlots = solution.ScheduleData[randomDay];

                    if (timeSlots.Count >= 2)
                    {
                        int index1 = random.Next(timeSlots.Count);
                        int index2 = random.Next(timeSlots.Count);

                        while (index2 == index1)
                        {
                            index2 = random.Next(timeSlots.Count);
                        }

                        var swapMove = new SwapTimeSlotMove(randomDay, index1, index2);

                        if (!tabuList.IsTabu(swapMove))
                        {
                            var temp = timeSlots[index1];
                            timeSlots[index1] = timeSlots[index2];
                            timeSlots[index2] = temp;

                            tabuList.AddTabu(swapMove);
                            isTabu = false;
                        }
                    }
                }
            }
        }


        public class SwapTimeSlotMove
        {
            public DayOfWeek Day { get; }
            public int Index1 { get; }
            public int Index2 { get; }

            public SwapTimeSlotMove(DayOfWeek day, int index1, int index2)
            {
                Day = day;
                Index1 = index1;
                Index2 = index2;
            }

            public override bool Equals(object? obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                var other = (SwapTimeSlotMove)obj;
                return Day == other.Day && Index1 == other.Index1 && Index2 == other.Index2;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Day, Index1, Index2);
            }
        }
    }
}
