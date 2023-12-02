
using System.Xml.Linq;
using TimeasyAPI.src.Models.ValueObjects.Enums;
using TimeasyCore.src.Models;
using TimeasyScheduler.src.Models;

namespace TimeasyCore.src.Mock
{
    public static class MockGenerator
    {
        private static Guid TimetableID = Guid.NewGuid();
        private static Guid normalRoomId = Guid.NewGuid();
        private static Guid infoLab = Guid.NewGuid();
        private static Guid engLab = Guid.NewGuid();

        public static CreateTimetableConfig GenarateFakeTimetableConfig(int seed)
        {


            Random random = new Random(seed);
            var timetable = new CreateTimetableConfig();
            List<Guid> roomsType = new List<Guid>
            {
                normalRoomId,
                infoLab,
                engLab,
            };

            List<Room> rooms = GenerateRooms(roomsType, 34, random);
            List<Course> courses = GenerateCoursesWithSubjects(10, random);
            List<Subject> subjects = courses.SelectMany(c => c.Subjects).ToList();
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
                    Capacity = random.Next(30,42) + 5
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
                CloseHour = new TimeOnly(22, 50),
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = false,
                Intervals = new()
                {
                    new Interval
                    {
                        Start = "11:50",
                        End =  "12:59"
                    }, 
                    new Interval
                    {
                        Start = "17:50",
                        End =  "18:50"
                    },
                }
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


        static List<Course> GenerateCoursesWithSubjects(int numberOfCourses, Random random)
        {
            var cienciaDaComputacaoID = Guid.NewGuid();
            var engenhariaDeControleAutomacaoID = Guid.NewGuid();
            var tecnologoEmADS = Guid.NewGuid();



            var algoritmosProgramacaoDeComputadores_SBVAPRC_ID = Guid.NewGuid();
            var calculoDiferencialIntegral1_SBVCDI1_ID = Guid.NewGuid();
            var circuitosDigitais_SBVCIDG_ID = Guid.NewGuid();
            var comunicacaoExpressao_SBVCOEX_ID = Guid.NewGuid();
            var geometriaAnaliticaVetores_SBVGEAV_ID = Guid.NewGuid();
            var introducaoSistemasInformacao_SBVINSI_ID = Guid.NewGuid();
            var responsabilidadeSocialSustentabilidade_SBVRESG_ID = Guid.NewGuid();
            var projetoExtensao1_SBVEXT1_ID = Guid.NewGuid();

            var algebraLinear_SBVALIN_ID = Guid.NewGuid();
            var arquiteturaOrganizacaoComputadores_SBVAORC_ID = Guid.NewGuid();
            var calculoDiferencialIntegral2_SBVCDI2_ID = Guid.NewGuid();
            var inglesFinsEspecificos_SBVINGL_ID = Guid.NewGuid();
            var matematicaDiscreta_SBVMADI_ID = Guid.NewGuid();
            var programacaoOrientadaObjetos_SBVPROO_ID = Guid.NewGuid();
            var projetoExtensao2_SBVEXT2_ID = Guid.NewGuid();

            var bancoDados_SBVBADD_ID = Guid.NewGuid();
            var cienciaTecnologiaSociedade_SBVCTSO_ID = Guid.NewGuid();
            var interacaoHumanoComputador_SBVINHC_ID = Guid.NewGuid();
            var nocoesDireitoDigital_SBVNDDG_ID = Guid.NewGuid();
            var paradigmasLinguagensProgramacao_SBVPALP_ID = Guid.NewGuid();
            var projetoAnaliseAlgoritmos_SBVPRAA_ID = Guid.NewGuid();
            var sistemasOperacionais_SBVSOPE_ID = Guid.NewGuid();
            var projetoExtensao3_SBVEXT3_ID = Guid.NewGuid();

            var calculoNumerico_BVCANU_ID = Guid.NewGuid();
            var estruturasDados_SBVESDD_ID = Guid.NewGuid();
            var laboratorioBancoDados_SBVLABD_ID = Guid.NewGuid();
            var linguagensFormaisAutomatos_SBVLIFA_ID = Guid.NewGuid();
            var probabilidadeEstatistica_SBVPREE_ID = Guid.NewGuid();
            var projetoAnaliseSoftware_SBVPRAS_ID = Guid.NewGuid();
            var projetoExtensao4_SBVEXT4_ID = Guid.NewGuid();

            var construcaoCompiladores_SBVCONC_ID = Guid.NewGuid();
            var engenhariaSoftware_SBVENGS_ID = Guid.NewGuid();
            var inteligenciaArtificial_SBVINAR_ID = Guid.NewGuid();
            var marcacaoLayoutWeb_SBVMWEB_ID = Guid.NewGuid();
            var organizacaoRecuperacaoInformacao_SBVORIN_ID = Guid.NewGuid();
            var visaoComputacional_SBVVISC_ID = Guid.NewGuid();
            var projetoExtensao5_SBVEXT5_ID = Guid.NewGuid();

            var eletronicaDigital1_SBVEDI1_ID = Guid.NewGuid();
            var introducaoProjetosEngenharia_SBVIPEN_ID = Guid.NewGuid();
            var informaticaLogicaProgramacao_SBVILPR_ID = Guid.NewGuid();
            var desenho1_SBVDES1_ID = Guid.NewGuid();
            var quimicaTeorica_SBVQTEO_ID = Guid.NewGuid();
            var quimicaExperimental_SBVQEXP_ID = Guid.NewGuid();
            var comunicacaoExpressao_SBVCEXP_ID = Guid.NewGuid();
            var responsabilidadeSocial_SBVRSOC_ID = Guid.NewGuid();

            var fisicaTeorica1_SBVFTE1_ID = Guid.NewGuid();
            var fisicaExperimental1_SBVFEX1_ID = Guid.NewGuid();
            var algoritmosProgramacao_SBVAPRO_ID = Guid.NewGuid();
            var desenho2_SBVDES2_ID = Guid.NewGuid();
            var inglesAcademico_SBVIACA_ID = Guid.NewGuid();
            var eletronicaDigital2_SBVEDI2_ID = Guid.NewGuid();
            var laboratorioEletronicaDigital1_SBVLED1_ID = Guid.NewGuid();

            var calculoDiferencialIntegral3_SBVCDI3_ID = Guid.NewGuid();
            var mecanicaGeral_SBVMGER_ID = Guid.NewGuid();
            var cienciasAmbiente_SBVCAMB_ID = Guid.NewGuid();
            var fisicaTeorica2_SBVFTE2_ID = Guid.NewGuid();
            var fisicaExperimental2_SBVFEX2_ID = Guid.NewGuid();
            var eletronicaDigital3_SBVEDI3_ID = Guid.NewGuid();
            var laboratorioEletronicaDigital2_SBVLED2_ID = Guid.NewGuid();
            var probabilidadeEstatistica_SBVPEST_ID = Guid.NewGuid();


            var cienciaMateriais_SBVCMAT_ID = Guid.NewGuid();
            var eletromagnetismo_SBVEEMAG_ID = Guid.NewGuid();
            var resistenciaMateriais_SBVRRMAT_ID = Guid.NewGuid();
            var circuitosEletricosCorrenteContinua_SBVCECC_ID = Guid.NewGuid();
            var processosFabricacao_SBVPFAB_ID = Guid.NewGuid();
            var eletronica1_SBVELE1_ID = Guid.NewGuid();
            var laboratorioEletronica1_SBVLEL1_ID = Guid.NewGuid();
            var ondasTermologiaOptica_SBVOTOP_ID = Guid.NewGuid();
            var fenomenosTransportes_SBVFTRA_ID = Guid.NewGuid();

            var hidraulicaPneumatica_SBVHPNE_ID = Guid.NewGuid();
            var laboratorioMaquinasComandosEletricos_SBVLMCE_ID = Guid.NewGuid();
            var administracaoGestao_SBVAGES_ID = Guid.NewGuid();
            var circuitosEletricosCorrenteAlternada_SBVCECA_ID = Guid.NewGuid();
            var elementosMaquinas_SBVEMAQ_ID = Guid.NewGuid();
            var eletronicaPotencia_SBVEMPO_ID = Guid.NewGuid();
            var instalacoesEletricasIndustriais_SBVIEIN_ID = Guid.NewGuid();
            var direitoEmpresarial_SBVDIRE_ID = Guid.NewGuid();
            var modelagemSistemas_SBVMSIS_ID = Guid.NewGuid();
            var eletronica2_SBVELE2_ID = Guid.NewGuid();

            var laboratorioEletronica2_SBVLEL2_ID = Guid.NewGuid();
            var conversao_ID = Guid.NewGuid();




            return new List<Course>()
           {
               new(){
                    Id = cienciaDaComputacaoID,
                    Name =  "Bacharelado em Ciência da Computação",
                    Turn = Turn.FullDay,
                    Monday = true,
                    Tuesday = true,
                    Wednesday = true,
                    Thursday = true,
                    Friday = true,
                    Saturday = false,
                    Subjects = new List<Subject>()
                    {
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "ALGORITMOS E PROGRAMAÇÃO DE COMPUTADORES",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = algoritmosProgramacaoDeComputadores_SBVAPRC_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 6,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "CÁLCULO DIFERENCIAL E INTEGRAL 1",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = calculoDiferencialIntegral1_SBVCDI1_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 6,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "CIRCUITOS DIGITAIS",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = circuitosDigitais_SBVCIDG_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 4,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                          new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "COMUNICAÇÃO E EXPRESSÃO",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = comunicacaoExpressao_SBVCOEX_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "GEOMETRIA ANALÍTICA E VETORES",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = geometriaAnaliticaVetores_SBVGEAV_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 3,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "INTRODUÇÃO AOS SISTEMAS DE INFORMAÇÃO",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = introducaoSistemasInformacao_SBVINSI_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "RESPONSABILIDADE SOCIAL E SUSTENTABILIDADE",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = responsabilidadeSocialSustentabilidade_SBVRESG_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PROJETO DE EXTENSÃO 1",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = projetoExtensao1_SBVEXT1_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "ÁLGEBRA LINEAR",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = algebraLinear_SBVALIN_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 3,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "ARQUITETURA E ORGANIZAÇÃO DE COMPUTADORES",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = arquiteturaOrganizacaoComputadores_SBVAORC_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "CÁLCULO DIFERENCIAL E INTEGRAL 2",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = calculoDiferencialIntegral2_SBVCDI2_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 5,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "INGLÊS PARA FINS ESPECÍFICOS",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = inglesFinsEspecificos_SBVINGL_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "MATEMÁTICA DISCRETA",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = matematicaDiscreta_SBVMADI_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PROGRAMAÇÃO ORIENTADA A OBJETOS",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = programacaoOrientadaObjetos_SBVPROO_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 6,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PROJETO DE EXTENSÃO 2",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = projetoExtensao2_SBVEXT2_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 2,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "BANCO DE DADOS",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = bancoDados_SBVBADD_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "CIÊNCIA, TECNOLOGIA E SOCIEDADE",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = cienciaTecnologiaSociedade_SBVCTSO_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 2,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "INTERAÇÃO HUMANO-COMPUTADOR",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = interacaoHumanoComputador_SBVINHC_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "NOÇÕES DE DIREITO DIGITAL",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = nocoesDireitoDigital_SBVNDDG_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 2,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PARADIGMAS DE LINGUAGENS DE PROGRAMAÇÃO",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = paradigmasLinguagensProgramacao_SBVPALP_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 6,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PROJETO E ANÁLISE DE ALGORITMOS",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = projetoAnaliseAlgoritmos_SBVPRAA_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 4,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "SISTEMAS OPERACIONAIS",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = sistemasOperacionais_SBVSOPE_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PROJETO DE EXTENSÃO 3",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = projetoExtensao3_SBVEXT3_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 2,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "CÁLCULO NUMÉRICO",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = calculoNumerico_BVCANU_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 3,
                            Period = 4,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "ESTRUTURAS DE DADOS",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = estruturasDados_SBVESDD_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 6,
                            Period = 4,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "LABORATÓRIO DE BANCO DE DADOS",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = laboratorioBancoDados_SBVLABD_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 4,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "LINGUAGENS FORMAIS E AUTÔMATOS",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = linguagensFormaisAutomatos_SBVLIFA_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 4,
                            Period = 4,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PROBABILIDADE E ESTATÍSTICA",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = probabilidadeEstatistica_SBVPREE_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 3,
                            Period = 4,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PROJETO E ANÁLISE DE SOFTWARE",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = projetoAnaliseSoftware_SBVPRAS_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 4,
                            Period = 4,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PROJETO DE EXTENSÃO 4",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = projetoExtensao4_SBVEXT4_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 2,
                            Period = 4,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "CONSTRUÇÃO DE COMPILADORES",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = construcaoCompiladores_SBVCONC_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 6,
                            Period = 5,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "ENGENHARIA DE SOFTWARE",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = engenhariaSoftware_SBVENGS_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 4,
                            Period = 5,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "INTELIGÊNCIA ARTIFICIAL",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = inteligenciaArtificial_SBVINAR_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 4,
                            Period = 5,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "MARCAÇÃO E LAYOUT PARA WEB",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = marcacaoLayoutWeb_SBVMWEB_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 5,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "ORGANIZAÇÃO E RECUPERAÇÃO DA INFORMAÇÃO",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = organizacaoRecuperacaoInformacao_SBVORIN_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 5,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "VISÃO COMPUTACIONAL",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = visaoComputacional_SBVVISC_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 5,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PROJETO DE EXTENSÃO 5",
                            CourseId = cienciaDaComputacaoID,
                            SubjectId = projetoExtensao5_SBVEXT5_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 2,
                            Period = 5,
                            StudentsCount = random.Next(20, 40) + 1
                        }

                    }
               },
                new(){
                    Id = engenhariaDeControleAutomacaoID,
                    Name =  "Engenharia de Controle e Automação",
                    Turn = Turn.FullDay,
                    Monday = true,
                    Tuesday = true,
                    Wednesday = true,
                    Thursday = true,
                    Friday = true,
                    Saturday = false,
                    Subjects = new List<Subject>()
                    {
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "CÁLCULO DIFERENCIAL E INTEGRAL 1",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = calculoDiferencialIntegral1_SBVCDI1_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 6,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "ELETRÔNICA DIGITAL 1",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = eletronicaDigital1_SBVEDI1_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "INTRODUÇÃO A PROJETOS DE ENGENHARIA",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = introducaoProjetosEngenharia_SBVIPEN_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 3,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "INFORMÁTICA E LÓGICA DE PROGRAMAÇÃO",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = informaticaLogicaProgramacao_SBVILPR_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "GEOMETRIA ANALÍTICA E VETORES",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = calculoDiferencialIntegral1_SBVCDI1_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 3,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "DESENHO 1",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = desenho1_SBVDES1_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 4,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "QUIMICA TEÓRICA",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = quimicaTeorica_SBVQTEO_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "QUIMICA EXPERIMENTAL",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = calculoDiferencialIntegral1_SBVCDI1_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "COMUNICAÇÃO E EXPRESÃO",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = comunicacaoExpressao_SBVCEXP_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "RESPONSABILIDADE SOCIAL",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = responsabilidadeSocial_SBVRSOC_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                         {
                            Id = Guid.NewGuid(),
                            Name = "CALCULO DIFERENCIAL E INTEGRAL 2",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = calculoDiferencialIntegral2_SBVCDI2_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 5,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                         {
                            Id = Guid.NewGuid(),
                            Name = "FISICA TEORIA 01",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = fisicaTeorica1_SBVFTE1_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 3,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                         {
                            Id = Guid.NewGuid(),
                            Name = "FISICA EXPERIMENTAL 01",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = fisicaExperimental1_SBVFEX1_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 4,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                         {
                            Id = Guid.NewGuid(),
                            Name = "ALGORITMOS E PROGRAMAÇÃO",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = algoritmosProgramacao_SBVAPRO_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 4,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                         {
                            Id = Guid.NewGuid(),
                            Name = "ÁLGEBRA LINEAR",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = algebraLinear_SBVALIN_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 3,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                           new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "DESENHO 2",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = desenho2_SBVDES2_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 3,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "INGLES ACADEMICO ",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = inglesAcademico_SBVIACA_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 2,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject() {
                            Id = Guid.NewGuid(),
                            Name = "ELETRONICA DIGITAL 2",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = eletronicaDigital2_SBVEDI2_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 3,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject() {
                            Id = Guid.NewGuid(),
                            Name = "LABORATORIO DE ELETRONICA DIGITAL 1",
                            CourseId = engenhariaDeControleAutomacaoID,
                            SubjectId = laboratorioEletronicaDigital1_SBVLED1_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 3,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                    }
                },
                new(){
                    Id = tecnologoEmADS,
                    Name =  "Tecnologo - Analise e Desenvolvimento de Sistemas",
                    Turn = Turn.Evening,
                    Monday = true,
                    Tuesday = true,
                    Wednesday = true,
                    Thursday = true,
                    Friday = true,
                    Saturday = false,
                    Subjects = new List<Subject>(){
                         new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "ALGORITMOS E PROGRAMAÇÃO DE COMPUTADORES",
                            CourseId = tecnologoEmADS,
                            SubjectId = algoritmosProgramacaoDeComputadores_SBVAPRC_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 6,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "CIRCUITOS DIGITAIS",
                            CourseId = tecnologoEmADS,
                            SubjectId = circuitosDigitais_SBVCIDG_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 4,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                          new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "COMUNICAÇÃO E EXPRESSÃO",
                            CourseId = tecnologoEmADS,
                            SubjectId = comunicacaoExpressao_SBVCOEX_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "INTRODUÇÃO AOS SISTEMAS DE INFORMAÇÃO",
                            CourseId = tecnologoEmADS,
                            SubjectId = introducaoSistemasInformacao_SBVINSI_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "RESPONSABILIDADE SOCIAL E SUSTENTABILIDADE",
                            CourseId = tecnologoEmADS,
                            SubjectId = responsabilidadeSocialSustentabilidade_SBVRESG_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 2,
                            Period = 1,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "ARQUITETURA E ORGANIZAÇÃO DE COMPUTADORES",
                            CourseId = tecnologoEmADS,
                            SubjectId = arquiteturaOrganizacaoComputadores_SBVAORC_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "INGLÊS PARA FINS ESPECÍFICOS",
                            CourseId = tecnologoEmADS,
                            SubjectId = inglesFinsEspecificos_SBVINGL_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "MATEMÁTICA DISCRETA",
                            CourseId = tecnologoEmADS,
                            SubjectId = matematicaDiscreta_SBVMADI_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PROGRAMAÇÃO ORIENTADA A OBJETOS",
                            CourseId = tecnologoEmADS,
                            SubjectId = programacaoOrientadaObjetos_SBVPROO_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 6,
                            Period = 2,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "BANCO DE DADOS",
                            CourseId = tecnologoEmADS,
                            SubjectId = bancoDados_SBVBADD_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "CIÊNCIA, TECNOLOGIA E SOCIEDADE",
                            CourseId = tecnologoEmADS,
                            SubjectId = cienciaTecnologiaSociedade_SBVCTSO_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 2,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "INTERAÇÃO HUMANO-COMPUTADOR",
                            CourseId = tecnologoEmADS,
                            SubjectId = interacaoHumanoComputador_SBVINHC_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "NOÇÕES DE DIREITO DIGITAL",
                            CourseId = tecnologoEmADS,
                            SubjectId = nocoesDireitoDigital_SBVNDDG_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = normalRoomId,
                            Complexity = SubjectComplexity.NORMAL,
                            WeeklyClassCount = 2,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PARADIGMAS DE LINGUAGENS DE PROGRAMAÇÃO",
                            CourseId = tecnologoEmADS,
                            SubjectId = paradigmasLinguagensProgramacao_SBVPALP_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 6,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "PROJETO E ANÁLISE DE ALGORITMOS",
                            CourseId = tecnologoEmADS,
                            SubjectId = projetoAnaliseAlgoritmos_SBVPRAA_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.HARD,
                            WeeklyClassCount = 4,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                        new Subject()
                        {
                            Id = Guid.NewGuid(),
                            Name = "SISTEMAS OPERACIONAIS",
                            CourseId = tecnologoEmADS,
                            SubjectId = sistemasOperacionais_SBVSOPE_ID,
                            TimetableId = TimetableID,
                            RoomTypeId = infoLab,
                            Complexity = SubjectComplexity.MEDIUM,
                            WeeklyClassCount = 4,
                            Period = 3,
                            StudentsCount = random.Next(20, 40) + 1
                        },
                    }
                }
           };

        }

    }
}
