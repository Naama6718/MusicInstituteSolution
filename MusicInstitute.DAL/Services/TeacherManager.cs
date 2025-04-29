//using MusicInstitute.DAL.Api;
//using MusicInstitute.DAL.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MusicInstitute.DAL.Services
{
//    internal class TeacherManager : ITeachers
//    {
//        private readonly DB_Manager _dbContext;
//        public TeacherManager()
//        {
//            _dbContext = new DB_Manager();
//        }
//        public async Task AddTeacher(Teacher teacher)
//        {
//            _dbContext.Teachers.Add(teacher);
//            await Task.CompletedTask;
//        }

//        public async Task DeleteTeacher(int teacherId)
//        {
//            var teacher = _dbContext.Teachers
//                .FirstOrDefault(t => t.TeacherId == teacherId);
//            if (teacher != null)
//            {
//                _dbContext.Teachers.Remove(teacher);
//            }
//            await Task.CompletedTask;
//        }

//        public async Task<List<int>> GetAllTeacherIds()
//        {
//            return await Task.FromResult(_dbContext.Teachers.Select(t => t.TeacherId).ToList());
//        }

//        public async Task<List<Teacher>> GetAllTeachers()
//        {
//            return await Task.FromResult(_dbContext.Teachers.ToList());
//        }


//        public async Task<Teacher> GetTeacherById(int teacherId)
//        {
//            var teacher = _dbContext.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
//            return await Task.FromResult(teacher);
//        }

//        public async Task<Teacher> GetTeacherByPassword(string password)
//        {
//            var teacher = _dbContext.Teachers.FirstOrDefault(t => t.TeacherPassword == password);
//            return await Task.FromResult(teacher);
//        }

//        public IEnumerable<Teacher> GetTeachersByExperience(int minYears, int maxYears)
//        {
//            return _dbContext.Teachers.Where(t => t.ExperienceYears >= minYears && t.ExperienceYears <= maxYears);
//        }


//        public IEnumerable<Teacher> GetTeachersByInstrument(string instrument)
//        {
//            return _dbContext.Teachers.Where(t => t.Instruments.Equals(instrument, StringComparison.OrdinalIgnoreCase));
//        }


//        public IDictionary<int, int> GetTeacherStatisticsByExperienceLevel()
//        {
//            return _dbContext.Teachers
//                .GroupBy(t => t.ExperienceYears)
//                .ToDictionary(g => g.Key, g => g.Count());
//        }

//        public IDictionary<string, int> GetTeacherStatisticsByInstrument()
//        {
//            return _dbContext.Teachers
//                .GroupBy(t => t.Instruments)
//                .ToDictionary(g => g.Key, g => g.Count());
//        }

//        public async Task<int> GetTotalTeachers()
//        {
//            return await Task.FromResult(_dbContext.Teachers.Count());
//        }


//        public void ResetPassword(int teacherId, string newPassword)
//        {
//            var teacher = _dbContext.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
//            if (teacher != null)
//            {
//                teacher.TeacherPassword = newPassword;
//            }
//        }

//        public IEnumerable<Teacher> SearchTeachers(string? name, string? instrument, int? experienceLevel)
//        {
//            return _dbContext.Teachers.Where(t =>
//                (string.IsNullOrEmpty(name) || t.FullName.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
//                (string.IsNullOrEmpty(instrument) || t.Instruments.Equals(instrument, StringComparison.OrdinalIgnoreCase)) &&
//                (!experienceLevel.HasValue || t.ExperienceYears == experienceLevel));
//        }

//        public async Task UpdateTeacher(Teacher teacher)
//        {
//            var existingTeacher = _dbContext.Teachers.FirstOrDefault(t => t.TeacherId == teacher.TeacherId);
//            if (existingTeacher != null)
//            {
//                existingTeacher.FullName = teacher.FullName;
//                existingTeacher.Instruments = teacher.Instruments;
//                existingTeacher.ExperienceYears = teacher.ExperienceYears;
//                existingTeacher.TeacherPassword = teacher.TeacherPassword;
//            }
//            await Task.CompletedTask;
//        }
//    }
}
