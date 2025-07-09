using MusicInstitute.BL.Models;

namespace MusicInstitute.BL.Api
{
    public interface ITeacher_Manager_BL
    {
        Task AddTeacher(TeacherDTO teacherDTO);
        Task ConfirmPasswordResetAsync(string email, string verificationCode, string newPassword);
        Task DeleteTeacherAsync(int teacherId);
        Task<List<TeacherDTO>> GetAllTeachers();
        Task<TeacherDTO> GetTeacherByEmailAsync(string email);
        Task<TeacherDTO> GetTeacherByIdAsync(int teacherId);
        Task<TeacherDTO> GetTeacherByNameAndPasswordAsync(string firstName, string lastName, string password);
        Task RequestPasswordResetAsync(string email);
        Task UpdateTeacherById(int teacherId, string currentPassword, TeacherDTO teacherDTO);
    }
}