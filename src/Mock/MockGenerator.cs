using TimeasyAPI.src.Models.ValueObjects.Enums;
using TimeasyCore.src.Models;

namespace TimeasyCore.src.Mock
{
    public static class MockGenerator
    {
        public static Timetable GenarateFakeTimetableConfig(int seed)
        {


            Random random = new Random(seed);
            Timetable timetable = new Timetable();
            List<Guid> roomsType = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
            };

            List<Course> courses = GenerateCourses(10, random);
            List<Room> rooms = GenerateRooms(roomsType, 10, random);
            List<Subject> subjects = GetSubjects(courses, 5, random);
            List<Teacher> teachers = GetTeachers(subjects, 10, random);

            timetable.Institute = GenerateInstitute(random);
            timetable.Courses.AddRange(courses);
            timetable.Rooms.AddRange(rooms);
            timetable.Courses.AddRange(courses);
            timetable.Teachers.AddRange(teachers);



            return timetable;
        }


        static List<Room> GenerateRooms(List<Guid> roomTypes, int numberOfRooms, Random random)
        {
            List<Room> rooms = new List<Room>();

            if (roomTypes.Count == 0)
            {
                return rooms; // Não há tipos de sala disponíveis.
            }

            int typesCount = roomTypes.Count;

            for (int i = 0; i < numberOfRooms; i++)
            {
                int typeIndex = i % typesCount;
                Guid roomType = roomTypes[typeIndex];

                Room room = new Room
                {
                    Id = Guid.NewGuid(),
                    RoomTypeId = roomType,
                    Capacity = random.Next(23) + 10
                };

                rooms.Add(room);
            }

            return rooms;
        }




        static Institute GenerateInstitute(Random random)
        {

            Institute institute = new Institute
            {
                Id = Guid.NewGuid(),
                OpenHour = new TimeOnly(07, 00),
                CloseHour = new TimeOnly(19, 00),
                Monday = random.Next(2) == 0,
                Tuesday = random.Next(2) == 0,
                Wednesday = random.Next(2) == 0,
                Thursday = random.Next(2) == 0,
                Friday = random.Next(2) == 0,
                Saturday = random.Next(2) == 0,
                Intervals = GenerateRandomInterval(random)
            };

            return institute;
        }

        static List<Teacher> GetTeachers(List<Subject> subjects, int count, Random random)
        {
            List<Teacher> teachers = new List<Teacher>();

            for (int i = 1; i <= count; i++)
            {
                Teacher teacher = new Teacher
                {
                    Id = Guid.NewGuid(),
                    WorkTime = GenerateRandomInterval(random),
                    PreferredSubjects = new List<Subject>()
                };

                int numPreferredSubjects = random.Next(1, 6); 

                for (int j = 0; j < numPreferredSubjects; j++)
                {
                    int randomIndex = random.Next(subjects.Count);
                    Subject randomSubject = subjects[randomIndex];

                    teacher.PreferredSubjects.Add(randomSubject);
                }

                teachers.Add(teacher);
            }

            return teachers;
        }

        static List<Interval> GenerateRandomInterval(Random random)
        {
            List<Interval> workTime = new List<Interval>();
            int numIntervals = random.Next(1, 4); 

            for (int i = 0; i < numIntervals; i++)
            {
                int startHour = random.Next(7, 18); 
                int endHour = random.Next(startHour + 1, 19); 

                Interval interval = new Interval
                {
                    Start = $"{startHour}:00",
                    End = $"{endHour}:00"
                };

                workTime.Add(interval);
            }

            return workTime;
        }

        static List<Subject> GetSubjects(List<Course> courses, int count, Random random)
        {
            List<Subject> subjects = new List<Subject>();

            foreach (Course course in courses)
            {
                for (int i = 1; i <= count; i++)
                {
                    Subject subject = new Subject
                    {
                        Id = Guid.NewGuid(),
                        CourseId = course.Id, 
                        RoomTypeId = Guid.NewGuid(),
                        Complexity = (SubjectComplexity)(i % 3 + 1),
                        WeeklyClassCount = i,
                        StudentsCount = i * 10
                    };

                    subjects.Add(subject);

                    course.Subjects.Add(subject);
                }
            }

            return subjects;
        }

        static List<Course> GenerateCourses(int numberOfCourses, Random random)
        {
            List<Course> courses = new List<Course>();

            for (int i = 0; i < numberOfCourses; i++)
            {
                Course course = new Course
                {
                    Id = Guid.NewGuid(),
                    Turn = (Turn)random.Next(3),
                    Monday = true,
                    Tuesday = true,
                    Wednesday = true,
                    Thursday = true,
                    Friday = true,
                    Saturday = false,
                    Subjects = new List<Subject>()
                };

                courses.Add(course);
            }

            return courses;
        }
    }
}
