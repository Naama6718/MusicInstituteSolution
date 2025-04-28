using MusicInstitute.DAL.Api;
using MusicInstitute.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicInstitute.DAL.Services
{
    internal class Students_Manager
    {
        private readonly DB_Manager _dbManager;
        public Students_Manager(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }

        Task<List<Student>> GetAllStudent()
        {
            List<Student> students = _dbManager.Students.ToList();
            if (students == null)
            {
                throw new InvalidOperationException("Students collection is not initialized.");
            }
              return Task.FromResult(students); 
        }
        void AddStudent(Student student)
        {
            _dbManager.Students.Add(student);
        }
        Task UpdateStudent(int _studentId, string _currentPassword, string _firstName = null, string _lastName = null, string _phone = null, string _email = null, string _instrument = null, int _level = 0, string _studentPassword = null)
        {
            return Task.Run(() =>
            {
                var existingStudent = _dbManager.Students.FirstOrDefault(s => s.StudentId == _studentId);
                if (existingStudent == null)
                    throw new Exception("Student not found");
                if (existingStudent.StudentPassword != _currentPassword)
                    throw new Exception("The Password is not correct");

                existingStudent.FirstName = _firstName != null ? _firstName : existingStudent.FirstName;
                existingStudent.LastName = _lastName != null ? _lastName : existingStudent.LastName;
                existingStudent.Phone = _phone != null ? _phone : existingStudent.Phone;
                existingStudent.Email = _email != null ? _email : existingStudent.Email;
                existingStudent.Instrument = _instrument != null ? _instrument : existingStudent.Instrument;
                existingStudent.Level = _level != 0 ? _level : existingStudent.Level;
                existingStudent.StudentPassword = _studentPassword != null ? _studentPassword : existingStudent.StudentPassword;

            });
        }
        Task DeleteStudent(int _studentId) 
        {
            return Task.Run(() =>
            {
                var existingStudent = _dbManager.Students.FirstOrDefault(s => s.StudentId == _studentId);
                if (existingStudent == null)
                    throw new Exception("Student not found");
                _dbManager.Students.Remove(existingStudent);
            });
        }

        public Task<Student> GetStudentById(int _studentId)
        {
            return Task.Run(() =>
            {
                var existingStudent = _dbManager.Students.FirstOrDefault(s => s.StudentId == _studentId);
                if (existingStudent == null)
                    throw new Exception("Student not found");
                return existingStudent; 
            });
        }

    }
}
