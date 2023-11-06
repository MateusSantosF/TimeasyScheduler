namespace TimeasyCore.src.Models
{
    public class Institute
    {
        public Guid Id { get; set; }
        public TimeOnly OpenHour { get; set; }
        public TimeOnly CloseHour { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public List<Interval> Intervals { get; set; }

        /// <summary>
        /// Retorna os dias de funcionamento da Instituição de Ensino
        /// </summary>
        /// <returns>Lista de dias de funcionamento</returns>
        public List<DayOfWeek> GetOpenDays()
        {
            var openDays = new List<DayOfWeek>();

            if (Monday) openDays.Add(DayOfWeek.Monday);
            if (Tuesday) openDays.Add(DayOfWeek.Tuesday);
            if (Wednesday) openDays.Add(DayOfWeek.Wednesday);
            if (Thursday) openDays.Add(DayOfWeek.Thursday);
            if (Friday) openDays.Add(DayOfWeek.Friday);
            if (Saturday) openDays.Add(DayOfWeek.Saturday);
            if (Sunday) openDays.Add(DayOfWeek.Sunday);


            return openDays;
        }
    }
}
