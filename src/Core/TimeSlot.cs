namespace TimeasyCore.src.Core
{
    public class TimeSlot :IComparable<TimeSlot>
    {
        public string SubjectName { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid TeacherID { get; set; }
        public Guid SubjectID { get; set; }
        public Guid CourseID { get; set; }
        public Guid RoomID { get; set; }
        public bool IsAllocated { get; set; } = false;
        public TimeSlot() { }
        public TimeSlot(TimeSlot original)
        {
            SubjectName = original.SubjectName;
            StartTime = original.StartTime;
            EndTime = original.EndTime;
            TeacherID = original.TeacherID;
            SubjectID = original.SubjectID;
            CourseID = original.CourseID;
            RoomID = original.RoomID;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            TimeSlot other = (TimeSlot)obj;
            return SubjectName == other.SubjectName &&
                   StartTime == other.StartTime &&
                   EndTime == other.EndTime &&
                   TeacherID == other.TeacherID &&
                   SubjectID == other.SubjectID &&
                   CourseID == other.CourseID &&
                   RoomID == other.RoomID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SubjectName, StartTime, EndTime, TeacherID, SubjectID, CourseID, RoomID);
        }

        public TimeSlot Clone()
        {
            return new TimeSlot(this);
        }

        public int CompareTo(TimeSlot? other)
        {
            if (other == null)
            {
                return 1;
            }

            int startTimeComparison = StartTime.CompareTo(other.StartTime);
            if (startTimeComparison != 0)
            {
                return startTimeComparison;
            }

            return EndTime.CompareTo(other.EndTime);
        }
    }
}