
﻿using MusicInstitute.DAL.Models;

namespace MusicInstitute.DAL.Api
{
    internal interface IStudents_Manager_DAL
    {
        Task AddStudent(Student student);
        Task DeleteStudent(int studentId);
        Task<List<Student>> GetAllStudent();
        Task<Student> GetStudentById(int studentId);
        Task UpdateStudent(int studentId, string currentPassword, string firstName = null, string lastName = null, string phone = null, string email = null, string instrument = null, int level = 0, string studentPassword = null);
    }
    }