using MusicInstitute.DAL.Models;

namespace MusicInstitute.DAL.Api
{
    internal interface ITeacher_Manager_DAL
    {
        Task AddTeacher(Student teacher);
        Task DeleteTeacher(int teacherId);
        Task<List<Instrument>> GetInstrumentsForTeacherAsync(int teacherId);
        Task<Teacher> GetTeacherById(int teacherId);
        Task<List<Teacher>> GetTeachersByExperience(int minYears, int maxYears);
        Task<int> GetTotalTeachers();
        void ResetPassword(int teacherId, string newPassword);
        Task UpdateTeacherAsync(int teacherId, string currentPassword, string fullName = null, string phone = null, string email = null, int experienceYears = 0, List<Instrument> instruments = null, List<AvailableLesson> availableLessons = null, List<BookedLesson> bookedLessons = null, List<PassedLesson> passedLessons = null);
    }
}