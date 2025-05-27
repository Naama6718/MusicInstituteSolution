using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicInstitute.BL.Models;
using MusicInstitute.BL.Services;
using MusicInstitute.DAL.Api;

namespace MusicInstitute.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordResetController : ControllerBase
    {
        private readonly PasswordResetService _passwordResetService;

        public PasswordResetController(PasswordResetService passwordResetService)
        {
            _passwordResetService = passwordResetService;
        }

        // בקשת שליחת קוד אימות למייל
        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode([FromBody] EmailRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest("Email is required.");

            var result = await _passwordResetService.SendVerificationCodeAsync(request.Email);
            if (!result)
                return NotFound("Email not found.");

            return Ok("Verification code sent.");
        }

        // אימות קוד שהמשתמש הזין
        //[HttpPost("verify-code")]
        //public IActionResult VerifyCode([FromBody] VerifyCodeRequest request)
        //{
        //    if (_passwordResetService.VerifyCode(request.Email, request.Code))
        //        return Ok("Code is valid.");
        //    else
        //        return BadRequest("Invalid or expired code.");
        //}

        // איפוס סיסמה עם קוד האימות
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            // קודם כל לוודא שהקוד נכון
            if (!_passwordResetService.VerifyCode(request.Email, request.Code))
                return BadRequest("Invalid or expired code.");

            // ואז לאפס סיסמה
            var success = await _passwordResetService.ResetPasswordAsync(request.Email, request.Code, request.NewPassword);
            if (!success)
                return BadRequest("Password reset failed.");

            return Ok("Password has been reset.");
        }

    }

    public class EmailRequest
    {
        public string Email { get; set; }
    }

    //public class VerifyCodeRequest
    //{
    //    public string Email { get; set; }
    //    public string Code { get; set; }
    //}

    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }

}

