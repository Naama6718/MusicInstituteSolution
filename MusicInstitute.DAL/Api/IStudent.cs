//using MusicInstitute.DAL.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MusicInstitute.DAL.Api
//{
//    internal interface IStudent
//    {
//        Task<List<Student>> GetAllStudent();
//        Task<List<Student>> GetAllStudentId();
//        Task AddStudent(Student student);
//        Task UpdateStudent(Student student);
//        Task DeleteStudent(int studentId);
//        Task<Student> GetStudentById(int studentId);
//        Task<Student> GetStudentByPassword(string password);
//        IEnumerable<Student> SearchStudents(string? name, string? instrument, int? level);
//        void ResetPassword(int studentId, string newPassword);
//        IDictionary<string, int> GetStudentStatisticsByInstrument();
//        IDictionary<int, int> GetStudentStatisticsByLevel();

//    }
//}
