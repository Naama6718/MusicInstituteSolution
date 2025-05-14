using MusicInstitute.BL.Models;

namespace MusicInstitute.BL.Api
{
    public interface IStudent_Manager_BL
    {
        Task AddStudent(StudentDTO studentDTO);
        Task ConfirmPasswordResetAsync(string email, string verificationCode, string newPassword);
        Task DeleteStudentAsync(int studentId);
        Task<List<StudentDTO>> GetAllStudents();
        Task<StudentDTO> GetStudentByEmailAsync(string email);
        Task<StudentDTO> GetStudentByIdAsync(int studentId);
        Task<StudentDTO> GetStudentByNameAndPasswordAsync(string firstName, string lastName, string password);
        Task<StudentDTO> GetStudentByNameAsync(string firstName, string lastName);
        Task<StudentDTO> GetStudentByPasswordAsync(string password);
        Task<StudentDTO> GetStudentByPhoneAsync(string phone);
        Task<List<StudentDTO>> GetStudentsByInstrumentAsync(string instrument);
        Task<List<StudentDTO>> GetStudentsByLevelAsync(int level);
        Task RequestPasswordResetAsync(string email);
        Task UpdateStudent(int studentId, string currentPassword, string firstName = null, string lastName = null, string phone = null, string email = null, string instrument = null, int level = 0, string studentPassword = null);
        Task UpdateStudentById(int studentId, string currentPassword, StudentDTO studentDTO);
    }
}