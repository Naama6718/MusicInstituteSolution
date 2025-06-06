﻿using MusicInstitute.DAL.Models;

namespace MusicInstitute.DAL.Api
{
    public interface IBookedLessons_Manager_DAL
    {
        Task AddLesson(BookedLesson lesson);
        Task<List<BookedLesson>> GetAllBookedLesson();
        Task<List<BookedLesson>> GetLessonsByInstrument(string instrumentName);
        Task<List<BookedLesson>> GetLessonsByStudent(string studentName);
        Task<BookedLesson?> GetLessonById(int lessonId);
        Task<List<BookedLesson>> GetLessonsByTeacher(string teacherName);
        Task<bool> RemoveLesson(int lessonId);
    }
}