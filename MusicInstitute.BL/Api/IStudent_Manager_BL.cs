using MusicInstitute.BL.Models;

namespace MusicInstitute.BL.Api
{
    public interface IStudent_Manager_BL
    {
        Task AddStudent(StudentDTO studentDTO);
        Task ConfirmPasswordReset(string email, string verificationCode, string newPassword);
        Task DeleteStudent(int studentId);
        Task<List<StudentDTO>> FindSimilarStudents(int studentId);
        Task<List<StudentDTO>> GetAllStudents();
        Task<string> GetMostPopularInstrument();
        Task<List<string>> GetRareInstruments(int maxStudentCountPerInstrument);
        Task<StudentDTO> GetStudentByEmail(string email);
        Task<StudentDTO> GetStudentById(int studentId);
        Task<StudentDTO> GetStudentByNameAndPassword(string firstName, string lastName, string password);
        Task<StudentDTO> GetStudentByName(string firstName, string lastName);
        Task<StudentDTO> GetStudentByPassword(string password);
        Task<StudentDTO> GetStudentByPhone(string phone);
        Task<List<StudentDTO>> GetStudentsByInstrument(string instrument);
        Task<List<StudentDTO>> GetStudentsByLevel(int level);
        Task RequestPasswordReset(string email);
        Task UpdateStudent(int studentId, string currentPassword, string firstName = null, string lastName = null, string phone = null, string email = null, string instrument = null, int level = 0, string studentPassword = null);
        Task UpdateStudentById(int studentId, string currentPassword, StudentDTO studentDTO);
    }
}