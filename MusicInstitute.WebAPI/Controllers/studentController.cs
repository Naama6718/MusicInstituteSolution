using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicInstitute.BL.Api;
using MusicInstitute.BL.Models;

namespace MusicInstitute.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent_Manager_BL _studentManagerBL;

        // קונסטרוקטור שמקבל את השירות לניהול תלמידים
        public StudentController(IStudent_Manager_BL studentManagerBL)
        {
            _studentManagerBL = studentManagerBL;
        }

        // בקשה להוסיף תלמיד חדש
        [HttpPost("add")]
        public async Task<IActionResult> AddStudent([FromBody] StudentDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName))
            {
                return BadRequest("All fields are required.");
            }

            try
            {
                await _studentManagerBL.AddStudent(model); // קריאה למתודה ללא הקצאה
                return Ok("Student added successfully."); // הודעת הצלחה
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
