using MusicInstitute.BL.Models;

namespace MusicInstitute.BL.Api
{
    public interface IPassedLessons_Manager_BL
    {
        Task AddLesson(PassedLessonDTO lessonDto);
        Task<List<PassedLessonDTO>> GetAllLessons();
        Task<List<PassedLessonDTO>> GetLessonsByInstrument(string instrumentName);
        Task<List<PassedLessonDTO>> GetLessonsByStudent(string studentName);
        Task<List<PassedLessonDTO>> GetLessonsByTeacher(string teacherName);
    }
}