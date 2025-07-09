using Microsoft.EntityFrameworkCore;
using MusicInstitute.DAL.Api;
using MusicInstitute.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInstitute.DAL.Services
{
    public class Teacher_Manager_DAL : ITeacher_Manager_DAL
    {
        private readonly DB_Manager _dbManager;

        public Teacher_Manager_DAL()
        {
            _dbManager = new DB_Manager();
        }

        public async Task<List<Teacher>> GetAllTeachers()
        {
            return await _dbManager.Teachers.ToListAsync();
        }

        public async Task AddTeacher(Teacher teacher)
        {
            await _dbManager.Teachers.AddAsync(teacher);
            await _dbManager.SaveChangesAsync();
        }

        public async Task DeleteTeacher(int teacherId)
        {
            var existingTeacher = await _dbManager.Teachers.FirstOrDefaultAsync(t => t.TeacherId == teacherId);
            if (existingTeacher == null)
                throw new Exception("Teacher not found");

            _dbManager.Teachers.Remove(existingTeacher);
            await _dbManager.SaveChangesAsync();
        }

        public async Task<Teacher> GetTeacherById(int teacherId)
        {
            var existingTeacher = await _dbManager.Teachers.FirstOrDefaultAsync(t => t.TeacherId == teacherId);
            if (existingTeacher == null)
                throw new Exception("Teacher not found");

            return existingTeacher;
        }
        public async Task<Teacher> GetTeacherByName(string name)
        {
            var existingTeacher = await _dbManager.Teachers.FirstOrDefaultAsync(t => t.FirstName +" " + t.LastName == name);
            if (existingTeacher == null)
                throw new Exception("Teacher not found");

            return existingTeacher;
        }


        public async Task<List<Teacher>> GetTeachersByExperience(int minYears, int maxYears)
        {
            return await _dbManager.Teachers
                .Where(t => t.ExperienceYears >= minYears && t.ExperienceYears <= maxYears)
                .ToListAsync();
        }

        public async Task<List<Instrument>> GetInstrumentsForTeacherAsync(int teacherId)
        {
            var existingTeacher = await _dbManager.Teachers
                .Include(t => t.Instruments)
                .FirstOrDefaultAsync(t => t.TeacherId == teacherId);

            if (existingTeacher == null)
                throw new KeyNotFoundException($"Teacher with ID {teacherId} not found.");

            return existingTeacher.Instruments.ToList();
        }

        public async Task<int> GetTotalTeachers()
        {
            return await _dbManager.Teachers.CountAsync();
        }

        public async Task ResetPassword(int teacherId, string newPassword)
        {
            var existingTeacher = await _dbManager.Teachers
                .FirstOrDefaultAsync(t => t.TeacherId == teacherId);

            if (existingTeacher == null)
                throw new KeyNotFoundException($"Teacher with ID {teacherId} not found.");

            // כאן אפשר להוסיף הצפנת סיסמה בעתיד
            existingTeacher.TeacherPassword = newPassword;
            await _dbManager.SaveChangesAsync();
        }

        public async Task UpdateTeacherAsync(
            int teacherId,
            string currentPassword,
            string firstName = null,
            string lastName = null,
            string phone = null,
            string email = null,
            int? experienceYears = null)
        {
            var existingTeacher = await _dbManager.Teachers
                .Include(t => t.Instruments)
                .Include(t => t.AvailableLessons)
                .Include(t => t.BookedLessons)
                .Include(t => t.PassedLessons)
                .FirstOrDefaultAsync(t => t.TeacherId == teacherId);

            if (existingTeacher == null)
                throw new KeyNotFoundException($"Teacher with ID {teacherId} not found.");

            if (existingTeacher.TeacherPassword != currentPassword)
                throw new UnauthorizedAccessException("Incorrect current password.");

            existingTeacher.FirstName = firstName ?? existingTeacher.FirstName;
            existingTeacher.LastName = lastName ?? existingTeacher.LastName;
            existingTeacher.Phone = phone ?? existingTeacher.Phone;
            existingTeacher.Email = email ?? existingTeacher.Email;

            if (experienceYears.HasValue)
                existingTeacher.ExperienceYears = experienceYears.Value;

            await _dbManager.SaveChangesAsync();
        }
    }
}
