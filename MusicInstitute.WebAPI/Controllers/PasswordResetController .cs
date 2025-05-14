using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicInstitute.BL.Models;
using MusicInstitute.BL.Services;
using MusicInstitute.DAL.Api;

namespace MusicInstitute.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
            private readonly Student_Manager_BL _Student_Manager_BL;

            public PasswordResetController(Student_Manager_BL studentmanager_BL)
            {
              _Student_Manager_BL = studentmanager_BL;
            }

            // שלב 1 - בקשה לאיפוס סיסמה (שליחת קוד לאימייל)
            //[HttpPost("request")]
            //public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetModel model)
            //{
            //    if (string.IsNullOrEmpty(model.Email))
            //    {
            //        return BadRequest("Email is required.");
            //    }

            //    try
            //    {
                    // קריאה לשליחת קוד איפוס
            //        await _Student_Manager_BL.RequestPasswordResetAsync(model.Email);
            //        return Ok("קוד איפוס נשלח למייל שלך.");
            //    }
            //    catch (Exception ex)
            //    {
            //        return BadRequest($"אירעה שגיאה: {ex.Message}");
            //    }
            //}

            // שלב 2 - אישור קוד איפוס סיסמה ועדכון הסיסמה
            //[HttpPost("confirm")]
            //public async Task<IActionResult> ConfirmPasswordReset([FromBody] PasswordResetRequest model)
            //{
            //    if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.VerificationCode) || string.IsNullOrEmpty(model.NewPassword))
            //    {
            //        return BadRequest("All fields are required.");
            //    }

            //    try
            //    {
                    // קריאה לאישור איפוס הסיסמה
            //        await _Student_Manager_BL.ConfirmPasswordResetAsync(model.Email, model.VerificationCode, model.NewPassword);
            //        return Ok("הסיסמה עודכנה בהצלחה.");
            //    }
            //    catch (Exception ex)
            //    {
            //        return BadRequest($"אירעה שגיאה: {ex.Message}");
            //    }
            //}
        }
    }

