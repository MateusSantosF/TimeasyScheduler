

namespace TimeasyCore.src.Core
{
    public class Schedule
    {
        public Dictionary<DayOfWeek, List<TimeSlot>> ScheduleData { get; }

        public Schedule(List<DayOfWeek> workDays)
        {
            ScheduleData = new Dictionary<DayOfWeek, List<TimeSlot>>();
            InitializeDaysOfWeek(workDays);
        }

        /// <summary>
        /// Inicializa o dicionário de dias da semana, com base nos dias de funcionamento da Instituição de Ensino
        /// </summary>
        /// <param name="workDays">Lista de dias de funcionamento da Instituição de Ensino</param>
        private void InitializeDaysOfWeek(List<DayOfWeek> workDays)
        {
            foreach (DayOfWeek day in workDays)
            {
                ScheduleData[day] = new List<TimeSlot>();
            }
        }

        /// <summary>
        /// Adiciona um TimeSlot no dia da semana específicado.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="timeSlot"></param>
        /// <returns>Retorna verdadeiro, caso foi posivel adicionar, e falso, caso o dia não seja um dia válido para a Instituição corrente.</returns>
        public bool AddTimeSlot(DayOfWeek day, TimeSlot timeSlot)
        {
            if (ScheduleData.ContainsKey(day))
            {
                ScheduleData[day].Add(timeSlot);
                return true;
            }

            return false;
        }
    }
}
