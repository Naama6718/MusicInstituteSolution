using Microsoft.EntityFrameworkCore;
using MusicInstitute.DAL.Api;
using MusicInstitute.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return await Task.FromResult(_dbManager.Teachers.Count());
        }

        public async Task ResetPassword(int teacherId, string newPassword)
        {
            var existingTeacher = await _dbManager.Teachers
                .Include(t => t.Instruments)
                .FirstOrDefaultAsync(t => t.TeacherId == teacherId);

            if (existingTeacher == null)
                throw new KeyNotFoundException($"Teacher with ID {teacherId} not found.");

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
            string teacherPassword = null,
            int experienceYears = 0,
            List<Instrument> instruments = null,
            List<AvailableLesson> availableLessons = null,
            List<BookedLesson> bookedLessons = null,
            List<PassedLesson> passedLessons = null)
        {
            var existingTeacher = await _dbManager.Teachers
                .Include(t => t.Instruments)
                .Include(t => t.AvailableLessons)
                .Include(t => t.BookedLessons)
                .Include(t => t.PassedLessons)
                .FirstOrDefaultAsync(t => t.TeacherId == teacherId);

            if (existingTeacher == null)
            {
                throw new KeyNotFoundException($"Teacher with ID {teacherId} not found.");
            }

            existingTeacher.FirstName = firstName ?? existingTeacher.FirstName;
            existingTeacher.LastName = lastName ?? existingTeacher.LastName;
            existingTeacher.Phone = phone ?? existingTeacher.Phone;
            existingTeacher.Email = email ?? existingTeacher.Email;
            existingTeacher.TeacherPassword = teacherPassword ?? existingTeacher.TeacherPassword;
            existingTeacher.ExperienceYears = experienceYears != 0 ? experienceYears : existingTeacher.ExperienceYears;

            if (instruments != null)
            {
                existingTeacher.Instruments.Clear();
                foreach (var instrument in instruments)
                {
                    existingTeacher.Instruments.Add(instrument);
                }
            }

            if (availableLessons != null)
            {
                existingTeacher.AvailableLessons.Clear();
                foreach (var lesson in availableLessons)
                {
                    existingTeacher.AvailableLessons.Add(lesson);
                }
            }

            if (bookedLessons != null)
            {
                existingTeacher.BookedLessons.Clear();
                foreach (var lesson in bookedLessons)
                {
                    existingTeacher.BookedLessons.Add(lesson);
                }
            }

            if (passedLessons != null)
            {
                existingTeacher.PassedLessons.Clear();
                foreach (var lesson in passedLessons)
                {
                    existingTeacher.PassedLessons.Add(lesson);
                }
            }

            await _dbManager.SaveChangesAsync();
        }

        public Task AddTeacher(Student teacher)
        {
            throw new NotImplementedException();
        }

        void ITeacher_Manager_DAL.ResetPassword(int teacherId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTeacherAsync(int teacherId, string currentPassword, string fullName = null, string phone = null, string email = null, int experienceYears = 0, List<Instrument> instruments = null, List<AvailableLesson> availableLessons = null, List<BookedLesson> bookedLessons = null, List<PassedLesson> passedLessons = null)
        {
            throw new NotImplementedException();
        }
    }
}
