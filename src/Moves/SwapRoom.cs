
using System.Threading.Tasks;
using TimeasyCore.src.Core;
using TimeasyScheduler.src.Core;

namespace TimeasyScheduler.src.Moves
{
    public class SwapRoom
    {
        public static void Execute(ref Schedule solution, TabuList<SwapRoomMove> tabuList, int currentBestWeight)
        {
            Random random = new Random();
            bool isTabu = true;

            while (isTabu)
            {
                DayOfWeek firstRandomDay = solution.ScheduleData.Keys.ElementAt(random.Next(solution.ScheduleData.Count));
                DayOfWeek secondRandomDay = solution.ScheduleData.Keys.ElementAt(random.Next(solution.ScheduleData.Count));


                if (solution.ScheduleData.ContainsKey(firstRandomDay) && solution.ScheduleData.ContainsKey(secondRandomDay))
                {
                    var timeSlotsFirstDay = solution.ScheduleData[firstRandomDay].Where(ts => ts.IsAllocated).ToList();
                    var timeSlotsSecondDay = solution.ScheduleData[secondRandomDay].Where(ts => ts.IsAllocated).ToList();

                    if (!timeSlotsFirstDay.Any() || !timeSlotsSecondDay.Any()) { continue; }

                    int firsrTimeslotIndex = random.Next(timeSlotsFirstDay.Count);
                    int secondTimeslotIndex = random.Next(timeSlotsSecondDay.Count);


                    var swapMove = new SwapRoomMove(timeSlotsFirstDay[firsrTimeslotIndex].RoomID, firsrTimeslotIndex, firstRandomDay);

                    if (!tabuList.IsTabu(swapMove) )
                    {
                        var temp = timeSlotsFirstDay[firsrTimeslotIndex].RoomID;
                        timeSlotsFirstDay[firsrTimeslotIndex].RoomID = timeSlotsSecondDay[secondTimeslotIndex].RoomID;
                        timeSlotsSecondDay[secondTimeslotIndex].RoomID = temp;

                        tabuList.AddTabu(swapMove);
                        isTabu = false;
                    }
               
                }
            }
        }


        public class SwapRoomMove
        {
            public DayOfWeek Day { get; }
            public Guid RoomID { get; }
            public int Index { get; }

            public SwapRoomMove(Guid roomId, int index, DayOfWeek dayOfWeek)
            {
                Day = dayOfWeek;
                RoomID = roomId;
                Index = index;
            }

            public override bool Equals(object? obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                var other = (SwapRoomMove)obj;
                return RoomID == other.RoomID && Index == other.Index && Day == other.Day;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(RoomID, Index, Day);
            }
        }
    }
}
