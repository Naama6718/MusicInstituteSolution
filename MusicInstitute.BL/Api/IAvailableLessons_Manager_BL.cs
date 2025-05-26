using MusicInstitute.BL.Models;

namespace MusicInstitute.BL.Api
{
    public interface IAvailableLessons_Manager_BL
    {
        Task AddLesson(AvailableLessonDTO lessonDto);
        Task<List<AvailableLessonDTO>> GetAllLessons();
        Task<List<AvailableLessonDTO>> GetLessonsByDate(DateOnly date);
        Task<List<AvailableLessonDTO>> GetLessonsByInstrument(string instrumentName);
        Task<List<AvailableLessonDTO>> GetLessonsByTeacher(string teacherName);
        Task<bool> IsLessonAvailable(DateOnly date, TimeOnly time);
        Task<bool> RemoveLesson(int lessonId);
    }
}