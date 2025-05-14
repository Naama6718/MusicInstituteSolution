using MusicInstitute.DAL.Models;

namespace MusicInstitute.DAL.Api
{
    public interface IPassedLessons_Manager_DAL
    {
        Task AddLesson(PassedLesson lesson);
        Task<List<PassedLesson>> GetAllLessons();
        Task<List<PassedLesson>> GetLessonsByInstrument(string instumentName);
        Task<List<PassedLesson>> GetLessonsByStudent(string studentName);
        Task<List<PassedLesson>> GetLessonsByTeacher(string teacherName);
    }
}