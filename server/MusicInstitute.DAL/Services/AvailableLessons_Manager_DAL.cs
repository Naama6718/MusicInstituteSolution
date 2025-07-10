using MusicInstitute.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicInstitute.DAL.Api;
namespace MusicInstitute.DAL.Services
{
    public class AvailableLessons_Manager_DAL : IAvailableLessons_Manager_DAL
    {
        private readonly DB_Manager _dbManager;

        public AvailableLessons_Manager_DAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }

        // הוספת שיעור חדש
        public async Task AddLesson(AvailableLesson lesson)
        {
            await _dbManager.AvailableLessons.AddAsync(lesson);
            await _dbManager.SaveChangesAsync();
        }

        // מחיקת שיעור לפי ID
        public async Task<bool> RemoveLesson(int lessonId)
        {
            var lesson = await _dbManager.AvailableLessons.FindAsync(lessonId);
            if (lesson != null)
            {
                _dbManager.AvailableLessons.Remove(lesson);
                await _dbManager.SaveChangesAsync();
                return true;
            }
            return false;
        }



        // קבלת כל השיעורים
        public async Task<List<AvailableLesson>> GetAllLessons()
        {
            return await _dbManager.AvailableLessons
                .Include(l => l.TeacherIdLessonsNavigation)
                .ToListAsync();
        }

        // קבלת שיעורים לפי מורה
        public async Task<List<AvailableLesson>> GetLessonsByTeacher(string teacherName)
        {
            return await _dbManager.AvailableLessons
                .Where(l => l.TeacherIdLessonsNavigation.FirstName + l.TeacherIdLessonsNavigation.LastName == teacherName)
                .ToListAsync();
        }

        //קבלת שיעורים לפי כלי נגינה
        public async Task<List<AvailableLesson>> GetLessonsByInstrumentment(string instrumentName)
        {
            return await _dbManager.AvailableLessons
                .Where(i => i.Kind.Equals(instrumentName))
                .ToListAsync();
        }

        // קבלת שיעורים לפי תאריך
        public async Task<List<AvailableLesson>> GetLessonsByDate(DateOnly date)
        {
            return await _dbManager.AvailableLessons
                .Where(l => l.LessonDate == date)
                .ToListAsync();
        }

        // בדיקת זמינות שיעור לפי תאריך ושעה
        public async Task<bool> IsLessonAvailable(DateOnly date, TimeOnly time)
        {
            return !await _dbManager.AvailableLessons
                .AnyAsync(l => l.LessonDate == date && l.LessonTime == time);
        }

        public async Task<AvailableLesson?> GetAvailableLessonById(int lessonId)
        {
            return await _dbManager.AvailableLessons
                .FirstOrDefaultAsync(l => l.LessonId == lessonId);
        }

    }

}