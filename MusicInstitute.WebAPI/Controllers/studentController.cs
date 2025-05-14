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

        // בקשה לעדכן תלמיד קיים
        [HttpPut("update/{studentId}")]
        public async Task<IActionResult> UpdateStudent(int studentId,string currentPasword, [FromBody] StudentDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName))
            {
                return BadRequest("All fields are required.");
            }
            try
            {
                await _studentManagerBL.UpdateStudent(studentId, currentPasword, model.FirstName, model.LastName, model.Phone, model.Email, model.Instrument, model.Level, model.StudentPassword); // קריאה למתודה ללא הקצאה
                return Ok("Student updated successfully."); // הודעת הצלחה
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        // בקשה למחוק תלמיד קיים
        [HttpDelete("delete/{studentId}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            try
            {
                await _studentManagerBL.DeleteStudent(studentId); // קריאה למתודה ללא הקצאה
                return Ok("Student deleted successfully."); // הודעת הצלחה
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        // בקשה לקבל את כל התלמידים
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentManagerBL.GetAllStudents(); // קריאה למתודה ללא הקצאה
                return Ok(students); // החזרת התלמידים
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        // בקשה לקבל תלמיד לפי מזהה
        [HttpGet("get/{studentId}")]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            try
            {
                var student = await _studentManagerBL.GetStudentById(studentId); // קריאה למתודה ללא הקצאה
                if (student == null)
                {
                    return NotFound("Student not found."); // הודעת שגיאה אם התלמיד לא נמצא
                }
                return Ok(student); // החזרת התלמיד
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        // בקשה להתחבר לתלמיד
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] StudentDTO model)
        //{
        //    if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.StudentPassword))
        //    {
        //        return BadRequest("Email and password are required.");
        //    }
        //    try
        //    {
        //        var student = await _studentManagerBL.Login(model); // קריאה למתודה ללא הקצאה
        //        if (student == null)
        //        {
        //            return Unauthorized("Invalid email or password."); // הודעת שגיאה אם ההתחברות נכשלה
        //        }
        //        return Ok(student); // החזרת התלמיד
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}
 
        // בקשה לשנות פרטי תלמיד
        //[HttpPut("updateDetails/{studentId}")]
        //public async Task<IActionResult> UpdateDetails(int studentId, [FromBody] StudentDTO model)
        //{
        //    if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName))
        //    {
        //        return BadRequest("All fields are required.");
        //    }
        //    try
        //    {
        //        await _studentManagerBL.UpdateDetails(studentId, model); // קריאה למתודה ללא הקצאה
        //        return Ok("Student details updated successfully."); // הודעת הצלחה
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}
        // בקשה לקבל את כל השיעורים של תלמיד
        //[HttpGet("getLessons/{studentId}")]
        //public async Task<IActionResult> GetLessons(int studentId)
        //{
        //    try
        //    {
        //        var lessons = await _studentManagerBL.GetLessons(studentId); // קריאה למתודה ללא הקצאה
        //        return Ok(lessons); // החזרת השיעורים
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}
        // בקשה לקבל את כל השיעורים הממתינים של תלמיד
        //[HttpGet("getPendingLessons/{studentId}")]
        //public async Task<IActionResult> GetPendingLessons(int studentId)
        //{
        //    try
        //    {
        //        var lessons = await _studentManagerBL.(studentId); // קריאה למתודה ללא הקצאה
        //        return Ok(lessons); // החזרת השיעורים
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}
        // בקשה לקבל את כל השיעורים המושלמים של תלמיד
        //[HttpGet("getCompletedLessons/{studentId}")]
        //public async Task<IActionResult> GetCompletedLessons(int studentId)
        //{
        //    try
        //    {
        //        var lessons = await _studentManagerBL.GetCompletedLessons(studentId); // קריאה למתודה ללא הקצאה
        //        return Ok(lessons); // החזרת השיעורים
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}
    }
}
