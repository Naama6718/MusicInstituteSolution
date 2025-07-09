using AutoMapper;
using MusicInstitute.BL.Api;
using MusicInstitute.BL.Models;
using MusicInstitute.DAL.Api;
using MusicInstitute.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicInstitute.BL.Services
{
    public class PassedLessons_Manager_BL : IPassedLessons_Manager_BL
    {
        private readonly IPassedLessons_Manager_DAL _dal;
        private readonly IMapper _mapper;

        public PassedLessons_Manager_BL(IPassedLessons_Manager_DAL dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }

        public async Task AddLesson(PassedLessonDTO lessonDto)
        {
            var lesson = _mapper.Map<PassedLesson>(lessonDto);
            await _dal.AddLesson(lesson);
        }

        public async Task<List<PassedLessonDTO>> GetAllLessons()
        {
            var lessons = await _dal.GetAllLessons();
            return _mapper.Map<List<PassedLessonDTO>>(lessons);
        }

        public async Task<List<PassedLessonDTO>> GetLessonsByTeacher(string teacherName)
        {
            var lessons = await _dal.GetLessonsByTeacher(teacherName);
            return _mapper.Map<List<PassedLessonDTO>>(lessons);
        }

        public async Task<List<PassedLessonDTO>> GetLessonsByInstrument(string instrumentName)
        {
            var lessons = await _dal.GetLessonsByInstrument(instrumentName);
            return _mapper.Map<List<PassedLessonDTO>>(lessons);
        }

        public async Task<List<PassedLessonDTO>> GetLessonsByStudent(string studentName)
        {
            var lessons = await _dal.GetLessonsByStudent(studentName);
            return _mapper.Map<List<PassedLessonDTO>>(lessons);
        }
    }
}
