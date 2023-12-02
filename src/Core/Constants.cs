namespace TimeasyScheduler.src.Core
{
    public static class Constants
    {
        public static readonly double CLASS_DURATION_IN_MINUTES = 50;
        public static int DAILY_GAP_MIN_TIME_IN_HOURS = 11;


        // CONSTRAINTS WEIGHT
        public static int DAILY_GAP_CONSTRAINT_WEIGHT = 1;
        public static int INSTITUTE_INTERVAL_CONFLICT_CONSTRAINT_WEIGHT = 1;
        public static int ROOM_CAPACITY_CONSTRAINT_WEIGHT = 1;
        public static int ROOM_TYPE_CONSTRAINT_WEIGHT = 1;
    }
}
