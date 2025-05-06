using MusicInstitute.DAL.Models;

namespace MusicInstitute.DAL.Api
{
    internal interface IBookedLessons_Manager_DAL
    {
        Task AddLesson(BookedLesson lesson);
        Task<List<BookedLesson>> GetAllBookedLesson();
        Task<List<BookedLesson>> GetLessonsByInstrument(string instrumentName);
        Task<List<BookedLesson>> GetLessonsByStudent(string studentName);
        Task<List<BookedLesson>> GetLessonsByTeacher(string teacherName);
        Task<bool> RemoveLesson(int lessonId);
    }
}