using TimeasyScheduler.src.Core.Interfaces;

namespace TimeasyCore.src.Models
{
    public class Room
    {
        public Guid Id { get; set; }

        public Guid RoomTypeId { get; set; }

        public int Capacity { get; set; }
    }
}
