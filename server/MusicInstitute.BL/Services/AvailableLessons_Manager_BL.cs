using AutoMapper;
using MusicInstitute.BL.Api;
using MusicInstitute.BL.Models;
using MusicInstitute.DAL.Api;
using MusicInstitute.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicInstitute.BL.Services
{
    public class AvailableLessons_Manager_BL : IAvailableLessons_Manager_BL
    {
        private readonly IAvailableLessons_Manager_DAL _availableLessonDal;
        private readonly IBookedLessons_Manager_DAL _bookedLessonDal;
        private readonly IPassedLessons_Manager_DAL _passedLessonDal;
        private readonly ITeacher_Manager_DAL _teacherDal;
        private readonly IMapper _mapper;

        public AvailableLessons_Manager_BL(IAvailableLessons_Manager_DAL dal, IMapper mapper, IBookedLessons_Manager_DAL bookedLessonDal, IPassedLessons_Manager_DAL passedLessonDal, ITeacher_Manager_DAL teacherDal)
        {
            _availableLessonDal = dal;
            _mapper = mapper;
            _bookedLessonDal = bookedLessonDal;
            _passedLessonDal = passedLessonDal;
            _teacherDal = teacherDal;
        }

        public async Task AddLesson(AvailableLessonDTO lessonDto)
        {
            var lesson = _mapper.Map<AvailableLesson>(lessonDto);
            await _availableLessonDal.AddLesson(lesson);
        }

        public async Task<bool> RemoveLesson(int lessonId)
        {
            return await _availableLessonDal.RemoveLesson(lessonId);
        }




        public async Task<List<AvailableLessonDTO>> GetLessonsByTeacher(string teacherName)
        {
            var lessons = await _availableLessonDal.GetLessonsByTeacher(teacherName);
            return _mapper.Map<List<AvailableLessonDTO>>(lessons);
        }

        public async Task<List<AvailableLessonDTO>> GetLessonsByInstrument(string instrumentName)
        {
            var lessons = await _availableLessonDal.GetLessonsByInstrumentment(instrumentName);
            return _mapper.Map<List<AvailableLessonDTO>>(lessons);
        }

        public async Task<List<AvailableLessonDTO>> GetLessonsByDate(DateOnly date)
        {
            var lessons = await _availableLessonDal.GetLessonsByDate(date);
            return _mapper.Map<List<AvailableLessonDTO>>(lessons);
        }

        public async Task<bool> IsLessonAvailable(DateOnly date, TimeOnly time)
        {
            return await _availableLessonDal.IsLessonAvailable(date, time);
        }
        public async Task<List<AvailableLessonDTO>> GetAllLessons()
        {
            var lessons = await _availableLessonDal.GetAllLessons();
            return _mapper.Map<List<AvailableLessonDTO>>(lessons);
        }
        /// 
        public async Task<List<AvailableLesson>> GetAvailableLessonsByTeacherNameAndKindAsync(string teacherName, string kind)
            {
                var teacher = await _teacherDal.GetTeacherByName(teacherName);
                if (teacher == null)
                    return new List<AvailableLesson>();

                var availableLessons = await _availableLessonDal.GetAllLessons();

                return availableLessons
                    .Where(l => l.TeacherIdLessons == teacher.TeacherId && l.Kind == kind)
                    .ToList();
            }

        public async Task<bool> BookSelectedLessonAsync(int selectedLessonId, int studentId)
        {
            var selectedLesson = await _availableLessonDal.GetAvailableLessonById(selectedLessonId);
            if (selectedLesson == null)
                return false;

            var bookedLesson = new BookedLesson
            {
                LessonId = selectedLesson.LessonId,
                LessonDate = selectedLesson.LessonDate,
                LessonTime = selectedLesson.LessonTime,
                Kind = selectedLesson.Kind,
                DurationMinutes = selectedLesson.DurationMinutes,
                TeacherIdLessons = selectedLesson.TeacherIdLessons,
                StudentIdLessons = studentId
            };
            

            await _bookedLessonDal.AddLesson(bookedLesson);
            await _availableLessonDal.RemoveLesson(selectedLesson.LessonId);

            return true;
        }

    }
}

