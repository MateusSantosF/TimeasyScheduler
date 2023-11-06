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
            List<Room> rooms = GenerateRooms(roomsType, 34, random);
            List<Subject> subjects = GetSubjects(courses, 10, random);
            List<Teacher> teachers = GetTeachers(subjects, 20, random);

            timetable.Institute = GenerateInstitute(random);
            timetable.Courses.AddRange(courses);
            timetable.Rooms.AddRange(rooms);
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
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = false,
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
                        Name = getRandomSubjectName(random),
                        Id = Guid.NewGuid(),
                        CourseId = course.Id, 
                        RoomTypeId = Guid.NewGuid(),
                        Complexity = (SubjectComplexity)(i % 3 + 1),
                        WeeklyClassCount = i,
                        StudentsCount = random.Next(20, 40) + 1
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
                    Name = getRandomCourseName(random),
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

        static string getRandomSubjectName(Random random)
        {
            List<string> subjectNames = new List<string>
            {
               "Introdução à Programação",
                "Cálculo I",
                "Álgebra Linear",
                "Estrutura de Dados",
                "Cálculo II",
                "Lógica de Programação",
                "Geometria Analítica",
                "Teoria da Computação",
                "Cálculo III",
                "Álgebra Abstrata",
                "Banco de Dados",
                "Probabilidade e Estatística",
                "Física Computacional",
                "Inteligência Artificial",
                "Teoria dos Grafos",
                "Matemática Discreta",
                "Redes de Computadores",
                "Programação Orientada a Objetos",
                "Equações Diferenciais",
                "Combinatória",
                "Arquitetura de Computadores",
                "Sistemas Operacionais",
                "Análise Numérica",
                "Álgebra Linear Numérica",
                "Otimização",
                "Segurança da Informação",
                "Sistemas Distribuídos",
                "Cálculo Numérico",
                "Teoria da Informação",
                "Matemática Computacional",
            };

            return subjectNames[random.Next(subjectNames.Count - 1)];

        }

        static string getRandomCourseName(Random random)
        {
            List<string> courseNames = new List<string>
            {
               "Bacharelado em Ciência da Computação",
                "Engenharia de Software",
                "Engenharia de Computação",
                "Sistemas de Informação",
                "Matemática Aplicada",
                "Ciência de Dados",
                "Engenharia Elétrica",
                "Engenharia de Telecomunicações",
                "Engenharia da Computação e Robótica",
                "Segurança da Informação",
            };

            return courseNames[random.Next(courseNames.Count - 1)];

        }
    }
}
