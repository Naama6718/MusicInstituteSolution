﻿using Microsoft.EntityFrameworkCore;
using MusicInstitute.DAL.Api;
using MusicInstitute.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstitute.DAL.Services
{
    public class PassedLessons_Manager_DAL : IPassedLessons_Manager_DAL
    {
        private readonly DB_Manager _dbManager;

        public PassedLessons_Manager_DAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }

        // הוספת שיעור חדש
        public async Task AddLesson(PassedLesson lesson)
        {
            await _dbManager.PassedLessons.AddAsync(lesson);
            await _dbManager.SaveChangesAsync();
        }

        // קבלת כל השיעורים
        public async Task<List<PassedLesson>> GetAllLessons()
        {
            return await _dbManager.PassedLessons
                .Include(l => l.TeacherIdLessonsNavigation)
                .ToListAsync();
        }

        public async Task<List<PassedLesson>> GetLessonsByTeacher(string teacherName)
        {
            return await _dbManager.PassedLessons
                .Where(l => l.TeacherIdLessonsNavigation.FirstName + " " +l.TeacherIdLessonsNavigation.LastName == teacherName)
                .ToListAsync();
        }

        public async Task<List<PassedLesson>> GetLessonsByInstrument(string instumentName)
        {
            return await _dbManager.PassedLessons
                .Where(i => i.Kind.Equals(instumentName))
                .ToListAsync();
        }

        public async Task<List<PassedLesson>> GetLessonsByStudent(string studentName)
        {
            return await _dbManager.PassedLessons
                .Where(l => l.StudentIdLessonsNavigation.FirstName + " " + l.StudentIdLessonsNavigation.LastName == studentName)
                .ToListAsync();
        }

    }
}
