﻿using MusicInstitute.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicInstitute.DAL.Api
{
    public interface ITeacher_Manager_DAL
    {
        Task AddTeacher(Teacher teacher);
        Task DeleteTeacher(int teacherId);
        Task<List<Instrument>> GetInstrumentsForTeacherAsync(int teacherId);
        Task<List<Teacher>> GetAllTeachers();
        Task<Teacher> GetTeacherById(int teacherId);
        Task<Teacher> GetTeacherByName(string name);
        Task<List<Teacher>> GetTeachersByExperience(int minYears, int maxYears);
        Task<int> GetTotalTeachers();
        Task ResetPassword(int teacherId, string newPassword);
        Task UpdateTeacherAsync(int teacherId, string currentPassword, string firstName = null, string lastName = null,
            string phone = null, string email = null, int? experienceYears = null);
    }
}
