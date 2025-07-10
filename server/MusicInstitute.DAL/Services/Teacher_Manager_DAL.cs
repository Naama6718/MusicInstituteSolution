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

        // קובץ: MusicInstitute.DAL/Services/Teacher_Manager_DAL.cs

        // קובץ: MusicInstitute.DAL/Services/Teacher_Manager_DAL.cs

        public async Task<Teacher> GetTeacherById(int teacherId)
        {
            // ודאי שיש לך using Microsoft.EntityFrameworkCore; בראש הקובץ

            var existingTeacher = await _dbManager.Teachers
                .Include(t => t.Instruments)
                .Include(t => t.AvailableLessons)
                // === כאן התיקון ===
                .Include(t => t.BookedLessons)
                    .ThenInclude(bl => bl.StudentIdLessonsNavigation) // השתמשי בשם הנכון
                .Include(t => t.PassedLessons)
                    .ThenInclude(pl => pl.StudentIdLessonsNavigation)  // השתמשי בשם הנכון
                .FirstOrDefaultAsync(t => t.TeacherId == teacherId);

            if (existingTeacher == null)
                throw new System.Exception("Teacher not found");

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

        // Teacher_Manager_DAL.cs

        // תמחק את הפונקציה הישנה UpdateTeacherAsync,
        // והחלף אותה בפונקציה החדשה והמשופרת הזאת:

        public async Task UpdateTeacherAsync(int teacherId, string currentPassword, Teacher updatedTeacherData)
        {
            var existingTeacher = await _dbManager.Teachers
                .FirstOrDefaultAsync(t => t.TeacherId == teacherId);

            if (existingTeacher == null)
                throw new KeyNotFoundException($"Teacher with ID {teacherId} not found.");

            // ודא שהסיסמה הנוכחית שהגיעה מהמשתמש נכונה
            if (existingTeacher.TeacherPassword != currentPassword)
                throw new UnauthorizedAccessException("Incorrect current password.");

            // עדכון השדות של המורה הקיים עם הנתונים החדשים
            // אנחנו לא מעדכנים את הסיסמה כאן. זה נעשה בפונקציה נפרדת.
            existingTeacher.FirstName = updatedTeacherData.FirstName;
            existingTeacher.LastName = updatedTeacherData.LastName;
            existingTeacher.Phone = updatedTeacherData.Phone;
            existingTeacher.Email = updatedTeacherData.Email;
            existingTeacher.ExperienceYears = updatedTeacherData.ExperienceYears;

            await _dbManager.SaveChangesAsync();
        }

        // הערה: תצטרך לעדכן גם את הממשק ITeacher_Manager_DAL בהתאם לחתימה החדשה.
        // פשוט שנה את חתימת הפונקציה גם שם.
    }
}
