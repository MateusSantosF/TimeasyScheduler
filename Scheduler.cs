

using TimeasyCore.src.Core;
using TimeasyCore.src.Mock;
using TimeasyCore.src.Models;

namespace TimeasyCore
{
    public class Scheduler
    {

        private const int MAX_INTERATION = 70000;

        public static void Main(string[] args)
        {
            var solutionRequest = MockGenerator.GenarateFakeTimetableConfig(252525);
            var bestSolution = CreateInitialRandomSolution(solutionRequest); // # Inicialização aleatória
            var bestCost = bestSolution.Validate(solutionRequest);

            for (int i = 0; i < MAX_INTERATION; i++)
            {
                var currentSolution = CreateInitialRandomSolution(solutionRequest);
                var currentCost = currentSolution.Validate(solutionRequest);
                if (bestCost.TotalWeight > currentCost.TotalWeight)
                {
                    Console.WriteLine($"CurrentSolution Weight: {currentCost.TotalWeight}");
                    bestSolution = currentSolution;
                    bestCost = currentCost;
                }

            }
         
            Console.WriteLine("\nFinish process");
            foreach (var kvp in bestCost.failedValidationMetrics)
            {
                Console.WriteLine($"Failed {kvp.Key}: {kvp.Value} times");
            }
            Console.Write($"Best solution weigth: {bestCost.TotalWeight}");
            Console.Read();
        }

        public void GenerateSolution()
        {

            int seed = 42;
            var solutionRequest = MockGenerator.GenarateFakeTimetableConfig(seed);

            var bestSolution = CreateInitialRandomSolution(solutionRequest); // # Inicialização aleatória
            var bestCost = bestSolution.Validate(solutionRequest); // # Avalie a solução inicial

            //Para i de 1 a iterações:
            //    SoluçãoCorrente ← ConstruirSoluçãoGulosaAleatória() # Construa uma solução parcial
            //    SoluçãoCorrente ← BuscaLocal(SoluçãoCorrente) # Melhore a solução parcial

            //    Se Custo(SoluçãoCorrente) < MelhorCusto:
            //        MelhorSolução ← SoluçãoCorrente
            //        MelhorCusto ← Custo(SoluçãoCorrente)

            //Retorne MelhorSolução, MelhorCusto
        }

        public static Schedule CreateInitialRandomSolution(Timetable solutionRequest)
        {
            var random = new Random();
            var emptyInitialSolution = new Schedule(solutionRequest.Institute.GetOpenDays(), solutionRequest.Institute.OpenHour, solutionRequest.Institute.CloseHour);
            //Console.WriteLine("EmptyInicialSolution generate...\n");
            //Console.WriteLine($"Institute Open {solutionRequest.Institute.OpenHour} and Close {solutionRequest.Institute.CloseHour}");
            //Console.WriteLine($"Total TimeSlots in One Day {emptyInitialSolution.ScheduleData.First().Value.Count}");

            //Console.WriteLine("Total available work days => " + emptyInitialSolution.ScheduleData.Keys.Count);
            
            //foreach( DayOfWeek day in emptyInitialSolution.ScheduleData.Keys)
            //{
            //    Console.WriteLine($"{day}");
            //}

            //Console.WriteLine("\n======================\n");


            var scheduleData = emptyInitialSolution.ScheduleData;

            var remainingSubjects = new List<Subject>(solutionRequest.Courses.SelectMany(c => c.Subjects).ToList());
            var availableTeachers = new List<Teacher>(solutionRequest.Teachers);
            var availableRooms = new List<Room>(solutionRequest.Rooms);
            
            //Console.WriteLine($"Total Courses => {solutionRequest.Courses.Count}");
            //Console.WriteLine($"Total Subjects => {remainingSubjects.Count}");
            //Console.WriteLine($"Total Teachers => {availableTeachers.Count}");
            //Console.WriteLine($"Total Rooms => {availableRooms.Count}");



            int iterationCount = 0;


            while (remainingSubjects.Count > 0 )
            {

                if (iterationCount >= MAX_INTERATION)
                {

                    throw new Exception("Error generate random Initial solution. Max iteration count.");
                }

                foreach (var day in emptyInitialSolution.ScheduleData.Keys)
                {

                    var currentDayTimeSlots = scheduleData[day];
                    Shuffle(currentDayTimeSlots, random);

                    foreach (TimeSlot timeSlot in currentDayTimeSlots)
                    {

                        if (remainingSubjects.Count == 0 )
                        {
                            return emptyInitialSolution;
                        }

                        int subjectIndex = random.Next(remainingSubjects.Count);
                        int teacherIndex = random.Next(availableTeachers.Count);
                        int roomIndex = random.Next(availableRooms.Count);

                        Subject subject = remainingSubjects[subjectIndex];
                        Teacher teacher = availableTeachers[teacherIndex];
                        Room room = availableRooms[roomIndex];

                        timeSlot.Name = subject.Name;
                        timeSlot.TeacherID = teacher.Id;
                        timeSlot.SubjectID = subject.Id;
                        timeSlot.CourseID = subject.CourseId;
                        timeSlot.RoomID = room.Id;
                        
                        remainingSubjects.RemoveAt(subjectIndex);
                    }
                }
                iterationCount++;
            }
            return emptyInitialSolution;
        }

        public static void Shuffle<T>(List<T> list, Random random)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }


}