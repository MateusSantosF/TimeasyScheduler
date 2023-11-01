

using TimeasyCore.src.Core;
using TimeasyCore.src.Mock;
using TimeasyCore.src.Models;

namespace TimeasyCore
{
    public class Class1
    {

        private const int MAX_INTERATION = 7000;

        public static void Main(string[] args)
        {
            var solutionRequest = MockGenerator.GenarateFakeTimetableConfig(32);
            var bestSolution = CreateInitialRandomSolution(solutionRequest); // # Inicialização aleatória

            Console.Write(bestSolution);
            Console.Read();

        }

        public void GenerateSolution()
        {

            int seed = 42;
            var solutionRequest = MockGenerator.GenarateFakeTimetableConfig(seed);

            var bestSolution = CreateInitialRandomSolution(solutionRequest); // # Inicialização aleatória
            //MelhorCusto ← CalcularCusto(MelhorSolução) # Avalie a solução inicial

            //Para i de 1 a iterações:
            //    SoluçãoCorrente ← ConstruirSoluçãoGulosaAleatória() # Construa uma solução parcial
            //    SoluçãoCorrente ← BuscaLocal(SoluçãoCorrente) # Melhore a solução parcial

            //    Se Custo(SoluçãoCorrente) < MelhorCusto:
            //        MelhorSolução ← SoluçãoCorrente
            //        MelhorCusto ← Custo(SoluçãoCorrente)

            //Retorne MelhorSolução, MelhorCusto
        }

        public static Schedule CreateInitialRandomSolution(src.Models.Timetable solutionRequest)
        {
            var random = new Random();
            var emptyInitialSolution = new Schedule(solutionRequest.Institute.GetOpenDays());
            Console.WriteLine("EmptyInicialSolution generate...");
            Console.WriteLine("Total available work days => " + emptyInitialSolution.ScheduleData.Keys);

            var scheduleData = emptyInitialSolution.ScheduleData;

            List<Subject> remainingSubjects = new List<Subject>(solutionRequest.Courses.SelectMany(c => c.Subjects).ToList());
            List<Teacher> remainingTeachers = new List<Teacher>(solutionRequest.Teachers);
            List<Room> rooms = new List<Room>(solutionRequest.Rooms);

            Console.WriteLine($"Total Subjects => {remainingSubjects.Count}");
            Console.WriteLine($"Total Teachers => {remainingTeachers.Count}");
            Console.WriteLine($"Total Rooms => {rooms.Count}");



            int iterationCount = 0;


            while (remainingSubjects.Count > 0 || remainingTeachers.Count > 0)
            {

                if (iterationCount >= MAX_INTERATION)
                {
                    throw new Exception("Error generate random Initial solution. Max iteration count.");
                }

                foreach (var day in emptyInitialSolution.ScheduleData.Keys)
                {
                    List<TimeSlot> timeSlots = scheduleData[day];

                    foreach (var timeSlot in timeSlots)
                    {
                        if (remainingSubjects.Count == 0 || remainingTeachers.Count == 0)
                        {
                            return emptyInitialSolution;
                        }

                        // Escolhe aleatoriamente as entidades
                        int subjectIndex = random.Next(remainingSubjects.Count);
                        int teacherIndex = random.Next(remainingTeachers.Count);
                        int roomIndex = random.Next(rooms.Count);

                        Subject subject = remainingSubjects[subjectIndex];
                        Teacher teacher = remainingTeachers[teacherIndex];
                        Room room = rooms[roomIndex];

                        TimeSpan startTime = GenerateRandomTime(random);
                        TimeSpan endTime = GenerateRandomTime(random);

                        var newTimeSlot = new TimeSlot
                        {
                            StartTime = startTime,
                            EndTime = endTime,
                            Name = subject.Name,
                            TeacherID = teacher.Id,
                            SubjectID = subject.Id,
                            CourseID = subject.CourseId,
                            RoomID = room.Id
                        };

                        timeSlots.Add(newTimeSlot);

                        remainingSubjects.RemoveAt(subjectIndex);
                        remainingTeachers.RemoveAt(teacherIndex);
                    }
                }
            }
            return emptyInitialSolution;
        }
        private static TimeSpan GenerateRandomTime(Random random)
        {
            int minutesInDay = 24 * 60;
            int randomMinutes = random.Next(minutesInDay);
            return TimeSpan.FromMinutes(randomMinutes);
        }
    }


}