using AutoMapper;
using MusicInstitute.BL.Api;
using MusicInstitute.BL.Models;
using MusicInstitute.DAL.Api;
using MusicInstitute.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicInstitute.BL.Services
{
    public class BookedLessons_Manager_BL : IBookedLessons_Manager_BL
    {
        private readonly IBookedLessons_Manager_DAL _dal;
        private readonly IPassedLessons_Manager_DAL _passedLessonsDal;
        private readonly IBookedLessons_Manager_DAL _bookedLessonsDal;
        private readonly IMapper _mapper;

        public BookedLessons_Manager_BL(IBookedLessons_Manager_DAL dal, IMapper mapper, IPassedLessons_Manager_DAL passedLessonsDal, IBookedLessons_Manager_DAL bookedLessonsDal)
        {
            _dal = dal;
            _mapper = mapper;
            _passedLessonsDal = passedLessonsDal;
            _bookedLessonsDal = bookedLessonsDal;
        }

        public async Task AddLesson(BookedLessonDTO lessonDto)
        {
            var lesson = _mapper.Map<BookedLesson>(lessonDto);
            await _dal.AddLesson(lesson);
        }

        public async Task<bool> RemoveLesson(int lessonId)
        {
            return await _dal.RemoveLesson(lessonId);
        }

        public async Task<List<BookedLessonDTO>> GetAllBookedLessons()
        {
            var lessons = await _dal.GetAllBookedLesson();
            return _mapper.Map<List<BookedLessonDTO>>(lessons);
        }

        public async Task<List<BookedLessonDTO>> GetLessonsByTeacher(string teacherName)
        {
            var lessons = await _dal.GetLessonsByTeacher(teacherName);
            return _mapper.Map<List<BookedLessonDTO>>(lessons);
        }

        public async Task<List<BookedLessonDTO>> GetLessonsByInstrument(string instrumentName)
        {
            var lessons = await _dal.GetLessonsByInstrument(instrumentName);
            return _mapper.Map<List<BookedLessonDTO>>(lessons);
        }

        public async Task<BookedLessonDTO> GetLessonById(int lessonId)
        {
            var lesson = await _dal.GetLessonById(lessonId);
            return _mapper.Map<BookedLessonDTO>(lesson);
        }

        public async Task<List<BookedLessonDTO>> GetLessonsByStudent(string studentName)
        {
            var lessons = await _dal.GetLessonsByStudent(studentName);
            return _mapper.Map<List<BookedLessonDTO>>(lessons);
        }
      
        //
        public async Task MovePastLessonsToPassedAsync()
        {
            var allBooked = await _bookedLessonsDal.GetAllBookedLesson();
            var now = DateTime.Now;

            // מסנן שיעורים שהתאריך + השעה שלהם כבר עברו
            var pastLessons = allBooked
                .Where(lesson =>
                {
                    var dateTime = lesson.LessonDate.ToDateTime(lesson.LessonTime);
                    return dateTime < now;
                })
                .ToList();

            foreach (var booked in pastLessons)
            {
                var passed = new PassedLesson
                {
                    LessonId = booked.LessonId,
                    LessonDate = booked.LessonDate,
                    LessonTime = booked.LessonTime,
                    Kind = booked.Kind,
                    StudentIdLessons = booked.StudentIdLessons,
                    TeacherIdLessons = booked.TeacherIdLessons,
                    DurationMinutes = booked.DurationMinutes,
                    // אין צורך למפות את הניווטים (Navigation Properties) - EF ימלא אותם לבד אם צריך
                };

                await _passedLessonsDal.AddLesson(passed);
                await _bookedLessonsDal.RemoveLesson(booked.LessonId);
            }
        }
    }
}