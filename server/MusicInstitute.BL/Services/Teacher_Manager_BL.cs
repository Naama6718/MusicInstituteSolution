using AutoMapper;
using MusicInstitute.DAL.Api;
using MusicInstitute.BL.Models;
using MusicInstitute.DAL.Models;
using MusicInstitute.BL.Email;
using MusicInstitute.BL.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInstitute.BL.Services
{
    public class Teacher_Manager_BL : ITeacher_Manager_BL
    {
        private readonly IMapper _mapper;
        private readonly ITeacher_Manager_DAL _teacherManagerDAL;
        private readonly IEmailService _emailService;
        private readonly Dictionary<string, PasswordResetRequest> _resetRequests = new();

        public Teacher_Manager_BL(IMapper mapper, ITeacher_Manager_DAL teacherManagerDAL, IEmailService emailService)
        {
            _mapper = mapper;
            _teacherManagerDAL = teacherManagerDAL;
            _emailService = emailService;
        }

        public async Task AddTeacher(TeacherDTO teacherDTO)
        {
            if (string.IsNullOrEmpty(teacherDTO.FirstName) || string.IsNullOrEmpty(teacherDTO.LastName))
                throw new ArgumentException("First name and last name cannot be null or empty.");

            if (string.IsNullOrEmpty(teacherDTO.Email))
                throw new ArgumentException("Teacher email is required.");

            var teacher = _mapper.Map<Teacher>(teacherDTO);
            await _teacherManagerDAL.AddTeacher(teacher);
        }

        public async Task<List<TeacherDTO>> GetAllTeachers()
        {
            var teachers = await _teacherManagerDAL.GetAllTeachers();
            if (!teachers.Any())
                throw new InvalidOperationException("No teachers found.");

            return _mapper.Map<List<TeacherDTO>>(teachers);
        }

        public async Task UpdateTeacherById(int teacherId, string currentPassword, TeacherDTO teacherDTO)
        {
            var teacher = _mapper.Map<Teacher>(teacherDTO);
            await _teacherManagerDAL.UpdateTeacherAsync(teacherId, currentPassword, teacher.FirstName, teacher.LastName, teacher.Phone, teacher.Email);
        }

        public async Task DeleteTeacherAsync(int teacherId)
        {
            await _teacherManagerDAL.DeleteTeacher(teacherId);
        }

        public async Task<TeacherDTO> GetTeacherByIdAsync(int teacherId)
        {
            var teacher = await _teacherManagerDAL.GetTeacherById(teacherId);
            if (teacher == null)
                throw new InvalidOperationException("Teacher not found.");

            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task<TeacherDTO> GetTeacherByEmailAsync(string email)
        {
            var teachers = await _teacherManagerDAL.GetAllTeachers();
            var teacher = teachers.FirstOrDefault(t => t.Email == email);
            if (teacher == null)
                throw new InvalidOperationException("Teacher not found.");

            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task<TeacherDTO> GetTeacherByNameAndPasswordAsync(string firstName, string lastName, string password)
        {
            var teachers = await _teacherManagerDAL.GetAllTeachers();
            var teacher = teachers.FirstOrDefault(t => t.FirstName == firstName && t.LastName == lastName && t.TeacherPassword == password);
            if (teacher == null)
                throw new InvalidOperationException("Teacher not found.");

            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task RequestPasswordResetAsync(string email)
        {
            var teachers = await _teacherManagerDAL.GetAllTeachers();
            var teacher = teachers.FirstOrDefault(t => t.Email == email);
            if (teacher == null)
                throw new InvalidOperationException("Teacher not found.");

            var verificationCode = new Random().Next(100000, 999999).ToString();
            var request = new PasswordResetRequest
            {
                Email = email,
                VerificationCode = verificationCode,
                Expiration = DateTime.UtcNow.AddMinutes(10)
            };

            _resetRequests[email] = request;

            string subject = "Password Reset Code - Music Institute";
            string body = $"Hello {teacher.FirstName},\n\nYour password reset code is: {verificationCode}\nThis code is valid for 10 minutes.\n\nThanks,\nMusic Institute \uD83C\uDFB5";

            await _emailService.SendEmailAsync(email, subject, body);
        }

        public async Task ConfirmPasswordResetAsync(string email, string verificationCode, string newPassword)
        {
            if (!_resetRequests.ContainsKey(email))
                throw new InvalidOperationException("No reset request found for this email.");

            var request = _resetRequests[email];

            if (request.Expiration < DateTime.UtcNow)
            {
                _resetRequests.Remove(email);
                throw new InvalidOperationException("Verification code has expired.");
            }

            if (request.VerificationCode != verificationCode)
                throw new InvalidOperationException("Invalid verification code.");

            var teachers = await _teacherManagerDAL.GetAllTeachers();
            var teacher = teachers.FirstOrDefault(t => t.Email == email);
            if (teacher == null)
                throw new InvalidOperationException("Teacher not found.");

            await _teacherManagerDAL.UpdateTeacherAsync(teacher.TeacherId, newPassword);

            _resetRequests.Remove(email);
        }
    }
}
