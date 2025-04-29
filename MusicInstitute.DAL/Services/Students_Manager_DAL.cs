using MusicInstitute.DAL.Api;
using MusicInstitute.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInstitute.DAL.Services
{
    internal class Students_Manager_DAL : IStudents_Manager_DAL
    {
        private readonly DB_Manager _dbManager;

        public Students_Manager_DAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task<List<Student>> GetAllStudent()
        {
            var students = await _dbManager.Students.ToListAsync();
            if (students == null)
            {
                throw new InvalidOperationException("Students collection is not initialized.");
            }
            return students;
        }

        public async Task AddStudent(Student student)
        {
            await _dbManager.Students.AddAsync(student);
            await _dbManager.SaveChangesAsync();
        }

        public async Task UpdateStudent(int studentId, string currentPassword, string firstName = null, string lastName = null, string phone = null, string email = null, string instrument = null, int level = 0, string studentPassword = null)
        {
            var existingStudent = await _dbManager.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
            if (existingStudent == null)
                throw new Exception("Student not found");

            existingStudent.FirstName = firstName ?? existingStudent.FirstName;
            existingStudent.LastName = lastName ?? existingStudent.LastName;
            existingStudent.Phone = phone ?? existingStudent.Phone;
            existingStudent.Email = email ?? existingStudent.Email;
            existingStudent.Instrument = instrument ?? existingStudent.Instrument;
            existingStudent.Level = level != 0 ? level : existingStudent.Level;
            existingStudent.StudentPassword = studentPassword ?? existingStudent.StudentPassword;

            await _dbManager.SaveChangesAsync();
        }

        public async Task DeleteStudent(int studentId)
        {
            var existingStudent = await _dbManager.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
            if (existingStudent == null)
                throw new Exception("Student not found");

            _dbManager.Students.Remove(existingStudent);
            await _dbManager.SaveChangesAsync();
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            var existingStudent = await _dbManager.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
            if (existingStudent == null)
                throw new Exception("Student not found");

            return existingStudent;
        }
    }
}
