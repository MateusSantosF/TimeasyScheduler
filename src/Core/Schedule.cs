using TimeasyScheduler.src.ConstraintsValidators;
using TimeasyScheduler.src.Core;
using TimeasyScheduler.src.Models;

namespace TimeasyCore.src.Core
{
    public class Schedule
    {

        public Dictionary<DayOfWeek, List<TimeSlot>> ScheduleData { get; }

        public readonly CreateTimetableConfig CreateTimetableConfig;

        public Schedule(List<DayOfWeek> workDays, TimeOnly startTime, TimeOnly endTime, ref CreateTimetableConfig createTimetableConfig)
        {
            ScheduleData = new Dictionary<DayOfWeek, List<TimeSlot>>();
            InitializeDaysOfWeek(workDays, startTime, endTime);
            CreateTimetableConfig = createTimetableConfig;
        }

        public Schedule(Schedule original)
        {
            // Copia dos dados ScheduleData
            ScheduleData = original.ScheduleData
                .ToDictionary(entry => entry.Key, entry => entry.Value.Select(slot => new TimeSlot(slot)).ToList());

            CreateTimetableConfig = original.CreateTimetableConfig;
        }

        /// <summary>
        /// Inicializa o dicionário de dias da semana, com base nos dias de funcionamento da Instituição de Ensino
        /// </summary>
        /// <param name="workDays">Lista de dias de funcionamento da Instituição de Ensino</param>
        private void InitializeDaysOfWeek(List<DayOfWeek> workDays, TimeOnly startTime, TimeOnly endTime)
        {

            foreach (DayOfWeek day in workDays)
            {
                ScheduleData[day] = GenerateTimeSlots(startTime, endTime);
            }
        }

        private List<TimeSlot> GenerateTimeSlots(TimeOnly startTime, TimeOnly endTime)
        {
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            while (startTime.AddMinutes(Constants.CLASS_DURATION_IN_MINUTES) < endTime)
            {
                var newTimeSlot = new TimeSlot
                {
                    StartTime = startTime,
                    EndTime = startTime.AddMinutes(Constants.CLASS_DURATION_IN_MINUTES)
                };

                timeSlots.Add(newTimeSlot);

                startTime = startTime.AddMinutes(Constants.CLASS_DURATION_IN_MINUTES);
            }

            return timeSlots;
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


        public SolutionCost GetCost()
        {
            return new SolutionCost().ValidateAll(this, CreateTimetableConfig);
        }

        public List<TimeSlot> GetTimeSlots()
        {
            return ScheduleData.Values.SelectMany(timeslot=>timeslot).ToList();
        }

        public Schedule Clone()
        {
            return new Schedule(this);
        }


    }
}
