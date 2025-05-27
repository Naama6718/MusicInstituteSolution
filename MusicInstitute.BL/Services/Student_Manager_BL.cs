using AutoMapper;
using MusicInstitute.DAL.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicInstitute.BL.Models;
using MusicInstitute.DAL.Models;
using MusicInstitute.BL.Email;
using MusicInstitute.BL.Api;

namespace MusicInstitute.BL.Services
{
    public class Student_Manager_BL : IStudent_Manager_BL
    {

        private readonly IMapper _mapper;
        private readonly IStudents_Manager_DAL _studentManagerDAL;
        private readonly IEmailService _emailService;
        private readonly Dictionary<string, PasswordResetRequest> _resetRequests = new();
        public Student_Manager_BL(IMapper mapper, IStudents_Manager_DAL studentManagerDAL, IEmailService emailService)
        {
            _mapper = mapper;
            _studentManagerDAL = studentManagerDAL;
            _emailService = emailService;

        }
        public async Task AddStudent(StudentDTO studentDTO)
        {
            if (string.IsNullOrEmpty(studentDTO.FirstName) || string.IsNullOrEmpty(studentDTO.LastName))
            {
                throw new ArgumentException("First name and last name cannot be null or empty.");
            }

            // ודא שיש סיסמה
            if (string.IsNullOrEmpty(studentDTO.StudentPassword))
            {
                throw new ArgumentException("Student password is required.");
            }

            try
            {
                // מיפוי מ-DTO ל-Entity
                var student = _mapper.Map<Student>(studentDTO);
                // הוספת התלמיד
                await _studentManagerDAL.AddStudent(student);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // פונקציה לקבלת כל התלמידים    
        public async Task<List<StudentDTO>> GetAllStudents()
        {
            try
            {
                var students = await _studentManagerDAL.GetAllStudent();
                if (students == null || !students.Any())
                {
                    throw new InvalidOperationException("No students found.");
                }
                return _mapper.Map<List<StudentDTO>>(students);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        // פונקציה לעדכון תלמיד
        public async Task UpdateStudent(int studentId,
            string currentPassword,
            string firstName = null,
            string lastName = null,
            string phone = null,
            string email = null,
            string instrument = null,
            int level = 0,
            string studentPassword = null)
        {

            try
            {
                await _studentManagerDAL.UpdateStudent(studentId, currentPassword, firstName, lastName, phone, email, instrument, level, studentPassword);
            }

            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        // פונקציה לעדכון תלמיד לפי ID  
        public async Task UpdateStudentById(int studentId, string currentPassword, StudentDTO studentDTO)
        {
            try
            {
                var student = _mapper.Map<Student>(studentDTO);
                await _studentManagerDAL.UpdateStudent(studentId, currentPassword, student.FirstName, student.LastName, student.Phone, student.Email, student.Instrument, student.Level, student.StudentPassword);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // פונקציה למחיקת תלמיד
        public async Task DeleteStudent(int studentId)
        {
            try
            {
                await _studentManagerDAL.DeleteStudent(studentId);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        // פונקציה לקבלת תלמיד לפי ID
        public async Task<StudentDTO> GetStudentById(int studentId)
        {
            try
            {
                var student = await _studentManagerDAL.GetStudentById(studentId);
                if (student == null)
                {
                    throw new InvalidOperationException("Student not found.");
                }
                return _mapper.Map<StudentDTO>(student);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        // פונקציה לקבלת תלמיד לפי שם
        public async Task<StudentDTO> GetStudentByName(string firstName, string lastName)
        {
            try
            {
                var students = await _studentManagerDAL.GetAllStudent();
                var student = students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
                if (student == null)
                {
                    throw new InvalidOperationException("Student not found.");
                }
                return _mapper.Map<StudentDTO>(student);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        // פונקציה לקבלת תלמיד לפי טלפון
        public async Task<StudentDTO> GetStudentByPhone(string phone)
        {
            try
            {
                var students = await _studentManagerDAL.GetAllStudent();
                var student = students.FirstOrDefault(s => s.Phone == phone);
                if (student == null)
                {
                    throw new InvalidOperationException("Student not found.");
                }
                return _mapper.Map<StudentDTO>(student);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        // פונקציה לקבלת תלמיד לפי אימייל
        public async Task<StudentDTO> GetStudentByEmail(string email)
        {
            try
            {
                var students = await _studentManagerDAL.GetAllStudent();
                var student = students.FirstOrDefault(s => s.Email == email);
                if (student == null)
                {
                    throw new InvalidOperationException("Student not found.");
                }
                return _mapper.Map<StudentDTO>(student);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        // פונקציה לקבלת תלמיד לפי כלי נגינה

        public async Task<List<StudentDTO>> GetStudentsByInstrument(string instrument)
        {
            try
            {
                var students = await _studentManagerDAL.GetAllStudent();
                var filteredStudents = students.Where(s => s.Instrument == instrument).ToList();
                if (filteredStudents == null || !filteredStudents.Any())
                {
                    throw new InvalidOperationException("No students found for this instrument.");
                }
                return _mapper.Map<List<StudentDTO>>(filteredStudents);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        // פונקציה לקבלת תלמיד לפי רמה
        public async Task<List<StudentDTO>> GetStudentsByLevel(int level)
        {
            try
            {
                var students = await _studentManagerDAL.GetAllStudent();
                var filteredStudents = students.Where(s => s.Level == level).ToList();
                if (filteredStudents == null || !filteredStudents.Any())
                {
                    throw new InvalidOperationException("No students found for this level.");
                }
                return _mapper.Map<List<StudentDTO>>(filteredStudents);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        // פונקציה לקבלת תלמיד לפי סיסמה
        public async Task<StudentDTO> GetStudentByPassword(string password)
        {
            try
            {
                var students = await _studentManagerDAL.GetAllStudent();
                var student = students.FirstOrDefault(s => s.StudentPassword == password);
                if (student == null)
                {
                    throw new InvalidOperationException("Student not found.");
                }
                return _mapper.Map<StudentDTO>(student);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        // פונקציה לקבלת תלמיד לפי שם וסיסמה
        public async Task<StudentDTO> GetStudentByNameAndPassword(string firstName, string lastName, string password)
        {
            try
            {
                var students = await _studentManagerDAL.GetAllStudent();
                var student = students.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName && s.StudentPassword == password);
                if (student == null)
                {
                    throw new InvalidOperationException("Student not found.");
                }
                return _mapper.Map<StudentDTO>(student);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<string>> GetRareInstruments(int maxStudentCountPerInstrument)
        {
            try
            {
                var students = await _studentManagerDAL.GetAllStudent();
                var rareInstruments = students
                    .GroupBy(s => s.Instrument)
                    .Where(g => g.Count() <= maxStudentCountPerInstrument)
                    .Select(g => g.Key)
                    .ToList();

                return rareInstruments;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        //public async Task<string> RecommendTeacherAsync(int studentId)
        //{
        //    try
        //    {
        //        var students = await _studentManagerDAL.GetAllStudent();
        //        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        //        if (student == null)
        //        {
        //            throw new InvalidOperationException("Student not found.");
        //        }

        //        var teachers = await _studentManagerDAL.GetAllStudent(); // נדרש DAL מתאים

        //        var match = teachers
        //            .Where(t => t.Instrument == student.Instrument && t.Level.Contains(student.Level))
        //            .OrderBy(t => t.CurrentStudentCount)
        //            .FirstOrDefault();

        //        return match?.Name ?? "No suitable teacher found.";
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        throw;
        //    }
        //}

        public async Task<string> GetMostPopularInstrument()
        {
            try
            {
                var students = await _studentManagerDAL.GetAllStudent();

                var mostPopular = students
                    .GroupBy(s => s.Instrument)
                    .OrderByDescending(g => g.Count())
                    .FirstOrDefault()?.Key;

                if (mostPopular == null)
                    throw new InvalidOperationException("No instruments found.");

                return mostPopular;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<StudentDTO>> FindSimilarStudents(int studentId)
        {
            try
            {
                var students = await _studentManagerDAL.GetAllStudent();
                var referenceStudent = students.FirstOrDefault(s => s.StudentId == studentId);

                if (referenceStudent == null)
                {
                    throw new InvalidOperationException("Reference student not found.");
                }

                var similarStudents = students
                    .Where(s => s.StudentId != studentId &&
                                s.Instrument == referenceStudent.Instrument &&
                                s.Level == referenceStudent.Level)
                    .ToList();

                return _mapper.Map<List<StudentDTO>>(similarStudents);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ChangePasswordByEmail(string email, string newPassword)
        {
            var students = await _studentManagerDAL.GetAllStudent();
            var student = students.FirstOrDefault(s => s.Email == email);
            if (student == null)
                return false;

            // קורא לפונקציה ב-DAL לשינוי סיסמה
            return await _studentManagerDAL.UpdateStudentPassword(student.StudentId, newPassword);
        }


    }
}
