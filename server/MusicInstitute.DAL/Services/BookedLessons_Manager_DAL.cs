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

        public async Task AddLesson(BookedLesson lesson)
        {
            // ודא שה-Id הוא אפס (ברירת מחדל)
            lesson.LessonId = 0; // או את השם המדויק של שדה המזהה שלך

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
        public async Task<BookedLesson?> GetLessonById(int lessonId)
        {
            return await _dbManager.BookedLessons
                .FirstOrDefaultAsync(l => l.LessonId == lessonId);
        }

        public async Task<List<BookedLesson>> GetLessonsByStudent(string studentName)
        {
            return await _dbManager.BookedLessons
                .Where(l => l.StudentIdLessonsNavigation.FirstName + " " + l.StudentIdLessonsNavigation.LastName == studentName)
                .ToListAsync();
        }

        public async Task<List<BookedLesson>> GetLessonsByStudentIdAsync(int studentId)
        {
            return await _dbManager.BookedLessons
                .AsNoTracking()
                .Where(b => b.StudentIdLessons == studentId)
                .Include(b => b.TeacherIdLessonsNavigation)   // לטעינת שם מורה
                .OrderBy(b => b.LessonDate)
                .ThenBy(b => b.LessonTime)
                .ToListAsync();
        }


        // BookedLessonsDal.cs  (שכבת DAL)
        public async Task<bool> BookSelectedLessonAsync(int lessonId, int studentId)
        {
            var selected = await _dbManager.AvailableLessons
                                    .AsTracking()
                                    .FirstOrDefaultAsync(l => l.LessonId == lessonId);

            if (selected == null || selected.TeacherIdLessons == null)
                return false;

            if (!await _dbManager.Students.AnyAsync(s => s.StudentId == studentId))
                return false;

            var booked = new BookedLesson
            {
                // אל תגדיר LessonId אם זו עמודת Identity בטבלת Booked_Lessons
                LessonDate = selected.LessonDate,
                LessonTime = selected.LessonTime,
                DurationMinutes = selected.DurationMinutes,
                TeacherIdLessons = selected.TeacherIdLessons,
                StudentIdLessons = studentId,
                Kind = selected.Kind
            };

            _dbManager.BookedLessons.Add(booked);
            _dbManager.AvailableLessons.Remove(selected);

            var rows = await _dbManager.SaveChangesAsync();   // rows > 0 → הצלחה
            Console.WriteLine($"📌 SaveChanges rows = {rows}");
            return rows > 0;
        }







    }
}
