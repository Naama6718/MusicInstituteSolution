using MusicInstitute.BL.Email;
using MusicInstitute.BL.Models;
using MusicInstitute.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusicInstitute.BL.Services
{
    public class PasswordResetService
    {
        private List<PasswordResetRequest> _requests = new();
        private readonly IEmailService _emailService;
        private readonly Student_Manager_BL _studentManager;
        private readonly string _filePath = "requests.json"; // קובץ לשמירה

        public PasswordResetService(IEmailService emailService, Student_Manager_BL studentManager)
        {
            _emailService = emailService;
            _studentManager = studentManager;
            LoadRequestsFromFile(); // טוען את הבקשות מהקובץ אם יש
        }

        public async Task<bool> SendVerificationCodeAsync(string email)
        {
            var student = await _studentManager.GetStudentByEmail(email);
            if (student == null)
                return false;

            var code = new Random().Next(100000, 999999).ToString();

            _requests.RemoveAll(r => r.Email == email);
            _requests.Add(new PasswordResetRequest
            {
                Email = email,
                VerificationCode = code,
                Expiration = DateTime.UtcNow.AddMinutes(10)
            });

            SaveRequestsToFile(); // שומר לקובץ

            string subject = "קוד אימות לאיפוס סיסמה";
            string body = $"שלום {student.FirstName},\nהקוד שלך הוא: {code}";

            await _emailService.SendEmailAsync(email, subject, body);
            return true;
        }

        public bool VerifyCode(string email, string code)
        {
            return _requests.Any(r =>
                r.Email == email.Trim() &&
                r.VerificationCode == code.Trim() &&
                r.Expiration > DateTime.UtcNow);
        }

        public async Task<bool> ResetPasswordAsync(string email, string code, string newPassword)
        {
            if (!VerifyCode(email, code))
                return false;

            var success = await _studentManager.ChangePasswordByEmail(email, newPassword);
            if (success)
            {
                _requests.RemoveAll(r => r.Email == email);
                SaveRequestsToFile(); // עידכון הקובץ לאחר האיפוס
            }

            return success;
        }

        private void SaveRequestsToFile()
        {
            var json = JsonSerializer.Serialize(_requests);
            File.WriteAllText(_filePath, json);
        }

        private void LoadRequestsFromFile()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                var loaded = JsonSerializer.Deserialize<List<PasswordResetRequest>>(json);
                _requests = loaded ?? new List<PasswordResetRequest>();
            }
        }
    }
}
