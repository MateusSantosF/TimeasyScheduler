

using System;
using TimeasyCore.src.Core;
using TimeasyCore.src.Mock;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.ConstraintsValidators;
using TimeasyScheduler.src.Core;
using TimeasyScheduler.src.Models;
using TimeasyScheduler.src.Moves;
using static TimeasyScheduler.src.Moves.SwapRoom;
using static TimeasyScheduler.src.Moves.SwapTimeSlots;

namespace TimeasyCore
{
    public class Scheduler
    {
        private const int MAX_INTERATION = 70000;
        private const int MAX_ENHANCED_INTERATION = 200;

        public static TabuList<SwapTimeSlotMove> swapTabuList = new TabuList<SwapTimeSlotMove>(20);
        public static TabuList<SwapRoomMove> swapRoomTabuList = new TabuList<SwapRoomMove>(20);

        public static void Main(string[] args)
        {
            CreateTimetableConfig solutionRequest = MockGenerator.GenarateFakeTimetableConfig(23);

            if (solutionRequest is null) return;

            var bestSolution = CreateInitialRandomSolution(solutionRequest); // # Inicialização aleatória
            var bestCost = bestSolution.GetCost();

            for (int i = 0; i < MAX_INTERATION && bestCost.TotalWeight > 0; i++)
            {
                var currentSolution = CreateRandomSolution(solutionRequest);
                var currentCost = currentSolution.GetCost();
                currentSolution = ApplyTabuSearch(currentSolution, currentCost, bestCost);

                if (currentCost.TotalWeight < bestCost.TotalWeight)
                {
                    bestSolution = currentSolution;
                    bestCost = currentCost;
                    Console.WriteLine($"CurrentBestSolution Weight: {currentCost.TotalWeight}");
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

        public static Schedule ApplyTabuSearch(Schedule currentSolution, SolutionCost currentCost, SolutionCost bestCost)
        {
            Random random = new Random();


            int enchancedIteration = 0;
            bool improvedSolution = false;
            //First-best-admissible strategy – FBA
            do
            {
                // Gerando Vizinhaça
                int swapType = random.Next(2);

                if (swapType == 0)
                    SwapTimeSlots.Execute(ref currentSolution, swapTabuList, bestCost.TotalWeight);
                if (swapType == 1)
                    SwapRoom.Execute(ref currentSolution, swapRoomTabuList, bestCost.TotalWeight);

                // avaliando custo da solucao melhorada
                var enhancedCost = currentSolution.GetCost();

                if (enhancedCost.TotalWeight < currentCost.TotalWeight)
                {
                    improvedSolution = true;
                    currentCost = enhancedCost;
                }
                enchancedIteration++;
            } while (improvedSolution == false && enchancedIteration < MAX_ENHANCED_INTERATION);

            return currentSolution;
        }

        public static Schedule CreateInitialRandomSolution(CreateTimetableConfig solutionRequest)
        {
            var random = new Random();
            var emptyInitialSolution = new Schedule(solutionRequest.Institute.GetOpenDays(), solutionRequest.Institute.OpenHour, solutionRequest.Institute.CloseHour,ref solutionRequest);
            Console.WriteLine("EmptyInicialSolution generate...\n");
            Console.WriteLine($"Institute Open {solutionRequest.Institute.OpenHour} and Close {solutionRequest.Institute.CloseHour}");
            Console.WriteLine($"Total TimeSlots in One Day {emptyInitialSolution.ScheduleData.First().Value.Count}");

            Console.WriteLine("Total available work days => " + emptyInitialSolution.ScheduleData.Keys.Count);

            foreach (DayOfWeek day in emptyInitialSolution.ScheduleData.Keys)
            {
                Console.WriteLine($"{day}");
            }

            Console.WriteLine("\n======================\n");


            var scheduleData = emptyInitialSolution.ScheduleData;

            var remainingSubjects = new List<Subject>(solutionRequest.Courses.SelectMany(c => c.Subjects).ToList());
            var availableTeachers = new List<Teacher>(solutionRequest.Teachers);
            var availableRooms = new List<Room>(solutionRequest.Rooms);

            Console.WriteLine($"Total Courses => {solutionRequest.Courses.Count}");
            Console.WriteLine($"Total Subjects => {remainingSubjects.Count}");
            Console.WriteLine($"Total Teachers => {availableTeachers.Count}");
            Console.WriteLine($"Total Rooms => {availableRooms.Count}");


            Console.WriteLine("\n======================\n");

            int iterationCount = 0;


            while (remainingSubjects.Count > 0 )
            {

                if (iterationCount >= MAX_INTERATION)
                    throw new Exception("Error generate random Initial solution. Max iteration count.");
                

                    DayOfWeek randomDay = scheduleData.Keys.ElementAt(random.Next(scheduleData.Count));
                    var currentDayTimeSlots = scheduleData[randomDay];
                    Shuffle(currentDayTimeSlots, random);

                    foreach (TimeSlot timeSlot in currentDayTimeSlots)
                    {

                    if (remainingSubjects.Count == 0)
                    {
                        return emptyInitialSolution;
                    }
                    if (timeSlot.IsAllocated) continue;

                        int subjectIndex = random.Next(remainingSubjects.Count);
                        int teacherIndex = random.Next(availableTeachers.Count);
                        int roomIndex = random.Next(availableRooms.Count);

                        Subject subject = remainingSubjects[subjectIndex];
                        Teacher teacher = availableTeachers[teacherIndex];
                        Room room = availableRooms[roomIndex];

                     

                        var roomTypeSearchIteration = 0;
                        while (room.RoomTypeId != subject.RoomTypeId && roomTypeSearchIteration < MAX_ENHANCED_INTERATION)
                        {
                            room = availableRooms[roomIndex];
                            roomTypeSearchIteration++;
                        }

                        timeSlot.SubjectName = subject.Name;
                        timeSlot.TeacherID = teacher.Id;
                        timeSlot.SubjectID = subject.Id;
                        timeSlot.CourseID = subject.CourseId;
                        timeSlot.RoomID = room.Id;
                        timeSlot.IsAllocated = true;
                                      
                        remainingSubjects.RemoveAt(subjectIndex);
                    }
                }
                iterationCount++;
            
            return emptyInitialSolution;
        }

        public static Schedule CreateRandomSolution(CreateTimetableConfig solutionRequest)
        {
            var random = new Random();
            var emptyInitialSolution = new Schedule(solutionRequest.Institute.GetOpenDays(), solutionRequest.Institute.OpenHour, solutionRequest.Institute.CloseHour,ref solutionRequest);

            var scheduleData = emptyInitialSolution.ScheduleData;

            var remainingSubjects = new List<Subject>(solutionRequest.Courses.SelectMany(c => c.Subjects).ToList());
            var availableTeachers = new List<Teacher>(solutionRequest.Teachers);
            var availableRooms = new List<Room>(solutionRequest.Rooms);

            int iterationCount = 0;


            while (remainingSubjects.Count > 0)
            {

                if (iterationCount >= MAX_INTERATION)
                    throw new Exception("Error generate random Initial solution. Max iteration count.");
                
                DayOfWeek randomDay = scheduleData.Keys.ElementAt(random.Next(scheduleData.Count));

              
                    var currentDayTimeSlots = scheduleData[randomDay];
                    Shuffle(currentDayTimeSlots, random);

                    foreach (TimeSlot timeSlot in currentDayTimeSlots)
                    {

                        if (remainingSubjects.Count == 0)
                        {
                            return emptyInitialSolution;
                        }

                        int subjectIndex = random.Next(remainingSubjects.Count);
                        int teacherIndex = random.Next(availableTeachers.Count);
                        int roomIndex = random.Next(availableRooms.Count);

                        Subject subject = remainingSubjects[subjectIndex];
                        Teacher teacher = availableTeachers[teacherIndex];
                        Room room =  availableRooms[roomIndex];

                        if (!timeSlot.IsAllocated)
                        {
                            timeSlot.SubjectName = subject.Name;
                            timeSlot.TeacherID = teacher.Id;
                            timeSlot.SubjectID = subject.Id;
                            timeSlot.CourseID = subject.CourseId;
                            timeSlot.RoomID = room.Id;
                            timeSlot.IsAllocated = true;
                        }

                        remainingSubjects.RemoveAt(subjectIndex);
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