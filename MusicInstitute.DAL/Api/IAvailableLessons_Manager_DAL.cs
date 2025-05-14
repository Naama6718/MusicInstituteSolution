using MusicInstitute.DAL.Models;

namespace MusicInstitute.DAL.Api
{
    public interface IAvailableLessons_Manager_DAL
    {
        Task AddLesson(AvailableLesson lesson);
        Task<List<AvailableLesson>> GetAllLessons();
        Task<List<AvailableLesson>> GetLessonsByDate(DateOnly date);
        Task<List<AvailableLesson>> GetLessonsByInstrumentment(string instrumentName);
        Task<List<AvailableLesson>> GetLessonsByTeacher(string teacherName);
        Task<bool> IsLessonAvailable(DateOnly date, TimeOnly time);
        Task<bool> RemoveLesson(int lessonId);
    }
}