using MusicInstitute.DAL.Models;

namespace MusicInstitute.DAL.Api
{
    internal interface IPassedLessons_Manager
    {
        Task AddLesson(PassedLesson lesson);
        Task<List<PassedLesson>> GetAllLessons();
        Task<List<PassedLesson>> GetLessonsByStudent(string studentName);
        Task<List<PassedLesson>> GetLessonsByTeacher(string teacherName);
    }
}