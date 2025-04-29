//using MusicInstitute.DAL.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
namespace MusicInstitute.DAL.Services
{
    //internal class AvailableLessons_Manager : IAvailableLessons_Manager
    //{
    //    private readonly DB_Manager _dbManager;

    //    public AvailableLessons_Manager(DB_Manager dbManager)
    //    {
    //        _dbManager = dbManager;
    //    }

    //    // הוספת שיעור חדש
    //    public async Task AddLesson(AvailableLesson lesson)
    //    {
    //        await _dbManager.AvailableLessons.AddAsync(lesson);
    //        await _dbManager.SaveChangesAsync();
    //    }

    //    // מחיקת שיעור לפי ID
    //    public async Task<bool> RemoveLesson(int lessonId)
    //    {
    //        var lesson = await _dbManager.AvailableLessons.FindAsync(lessonId);
    //        if (lesson != null)
    //        {
    //            _dbManager.AvailableLessons.Remove(lesson);
    //            await _dbManager.SaveChangesAsync();
    //            return true;
    //        }
    //        return false;
    //    }



    //    // קבלת כל השיעורים
    //    public async Task<List<AvailableLesson>> GetAllLessons()
    //    {
    //        return await _dbManager.AvailableLessons
    //            .Include(l => l.TeacherIdLessonsNavigation)
    //            .ToListAsync();
    //    }

    //    // קבלת שיעורים לפי מורה
    //    public async Task<List<AvailableLesson>> GetLessonsByTeacher(string teacherName)
    //    {
    //        return await _dbManager.AvailableLessons
    //            .Where(l => l.TeacherIdLessonsNavigation.FullName == teacherName)
    //            .ToListAsync();
    //    }

    //    // קבלת שיעורים לפי תאריך
    //    public async Task<List<AvailableLesson>> GetLessonsByDate(DateOnly date)
    //    {
    //        return await _dbManager.AvailableLessons
    //            .Where(l => l.LessonDate == date)
    //            .ToListAsync();
    //    }

    //    // בדיקת זמינות שיעור לפי תאריך ושעה
    //    public async Task<bool> IsLessonAvailable(DateOnly date, TimeOnly time)
    //    {
    //        return !await _dbManager.AvailableLessons
    //            .AnyAsync(l => l.LessonDate == date && l.LessonTime == time);
    //    }
    //}

}