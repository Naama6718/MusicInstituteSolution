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
    public class BookedLessons_Manager_DAL : IBookedLessons_Manager_DAL
    {
        private readonly DB_Manager _dbManager;
        public BookedLessons_Manager_DAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }

        //הוספת שיעור חדש
        public async Task AddLesson(BookedLesson lesson)
        {
            await _dbManager.BookedLessons.AddAsync(lesson);
            await _dbManager.SaveChangesAsync();
        }

        // מחיקת שיעור לפי ID
        public async Task<bool> RemoveLesson(int lessonId)
        {
            var lesson = await _dbManager.BookedLessons.FindAsync(lessonId);
            if (lesson != null)
            {
                _dbManager.BookedLessons.Remove(lesson);
                await _dbManager.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<BookedLesson>> GetAllBookedLesson()
        {
            var BookedLessons = await _dbManager.BookedLessons.ToListAsync();
            if (BookedLessons == null)
            {
                throw new InvalidOperationException("BookedLessons collection is not initialized.");
            }
            return BookedLessons;
        }

        public async Task<List<BookedLesson>> GetLessonsByTeacher(string teacherName)
        {
            return await _dbManager.BookedLessons
                .Where(l => l.TeacherIdLessonsNavigation.FirstName + " " + l.TeacherIdLessonsNavigation.LastName == teacherName)
                .ToListAsync();
        }


        public async Task<List<BookedLesson>> GetLessonsByInstrument(string instrumentName)
        {
            return await _dbManager.BookedLessons
                .Where(i => i.Kind.Equals(instrumentName))
                .ToListAsync();
        }

        public async Task<List<BookedLesson>> GetLessonsByStudent(string studentName)
        {
            return await _dbManager.BookedLessons
                .Where(l => l.StudentIdLessonsNavigation.FirstName + " " + l.StudentIdLessonsNavigation.LastName == studentName)
                .ToListAsync();
        }





    }
}
