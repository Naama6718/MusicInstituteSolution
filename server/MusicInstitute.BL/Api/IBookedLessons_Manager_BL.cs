using MusicInstitute.BL.Models;

namespace MusicInstitute.BL.Api
{
    public interface IBookedLessons_Manager_BL
    {
        Task<List<BookedLessonDTO>> GetBookedLessonsAsync(int studentId);
        Task MovePastLessonsToPassedAsync();
        Task<bool> BookSelectedLessonAsync(int lessonId, int studentId);
        Task AddLesson(BookedLessonDTO lessonDto);
        Task<List<BookedLessonDTO>> GetAllBookedLessons();
        Task<BookedLessonDTO> GetLessonById(int lessonId);
        Task<List<BookedLessonDTO>> GetLessonsByInstrument(string instrumentName);
        Task<List<BookedLessonDTO>> GetLessonsByStudent(string studentName);
        Task<List<BookedLessonDTO>> GetLessonsByTeacher(string teacherName);
      //  Task<bool> MoveToPassedLesson(int bookedLessonId);
        Task<bool> RemoveLesson(int lessonId);
    }
}