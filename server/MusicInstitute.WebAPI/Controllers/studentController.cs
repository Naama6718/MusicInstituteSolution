using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicInstitute.BL.Api;
using MusicInstitute.BL.Models;
using MusicInstitute.DAL.Models;
using System.Text.Json;

namespace MusicInstitute.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent_Manager_BL _studentManagerBL;
        private readonly IBookedLessons_Manager_BL _lessonManagerBL;
        private readonly ILogger<StudentController> _logger; // הוספת לוגר
        // הוספת שירות לניהול שיעורים

        // קונסטרוקטור שמקבל את השירות לניהול תלמידים
        public StudentController(IStudent_Manager_BL studentManagerBL, IBookedLessons_Manager_BL lessonManagerBL, ILogger<StudentController> logger)
        {
            _studentManagerBL = studentManagerBL;
            _lessonManagerBL = lessonManagerBL;
            _logger = logger;
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

        //// בקשה לעדכן תלמיד קיים
        //[HttpPut("update/{studentId}")]
        //public async Task<IActionResult> UpdateStudent(int studentId,string currentPasword, [FromBody] StudentDTO model)
        //{
        //    if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName))
        //    {
        //        return BadRequest("All fields are required.");
        //    }
        //    try
        //    {
        //        await _studentManagerBL.UpdateStudent(studentId, currentPasword, model.FirstName, model.LastName, model.Phone, model.Email, model.Instrument, model.Level, model.StudentPassword); // קריאה למתודה ללא הקצאה
        //        return Ok("Student updated successfully."); // הודעת הצלחה
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error: {ex.Message}");
        //    }
        //}
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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] StudentDTO model)
        {
            Console.WriteLine($"Login attempt: FirstName='{model.FirstName}', LastName='{model.LastName}', Password='{model.StudentPassword}'");

            if (string.IsNullOrWhiteSpace(model.FirstName) ||
                string.IsNullOrWhiteSpace(model.LastName) ||
                string.IsNullOrWhiteSpace(model.StudentPassword))
            {
                return BadRequest("Username and password are required.");
            }

            string username = $"{model.FirstName} {model.LastName}";
            string password = model.StudentPassword;

            var student = await _studentManagerBL.Login(username, password);

            if (student == null)
                return Unauthorized("Invalid username or password.");

            Console.WriteLine($"==> Logged-in entity: {JsonSerializer.Serialize(student)}"); // כאן רואים בדיוק מה השרת מחזיר

            return Ok(student);
        }


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
        //
        [HttpGet("{studentId:int}/bookedLessons")]
        public async Task<IActionResult> GetBookedLessons(int studentId)
        {
            try
            {
                var lessons = await _studentManagerBL.GetBookedLessonsAsync(studentId);
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ שגיאה בשליפת שיעורים:");
                Console.WriteLine(ex.ToString()); // מדפיס את כל ה־stack trace
                return StatusCode(500, "שגיאה בשרת");
            }
        }

        [HttpGet("{studentId:int}/passedLessons")]
        public async Task<IActionResult> GetPassedLessons(int studentId)
        {
            try
            {
                var lessons = await _studentManagerBL.GetPassedLessonsAsync(studentId);
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ שגיאה בשליפת שיעורים שעברו:");
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "שגיאה בשרת");
            }
        }

        // Controller – בקשת POST עם גוף JSON, בלי query-string
        [HttpPost("bookLesson")]
        public async Task<IActionResult> BookLesson([FromBody] BookedLessonDTO dto)
        {
            Console.WriteLine($"===> Booking request received:");
            Console.WriteLine($"     lessonId: {dto.LessonId}");
            Console.WriteLine($"     studentId: {dto.StudentIdLessons}");

            if (dto is null) return BadRequest("Missing body");

            bool ok = await _lessonManagerBL.BookSelectedLessonAsync(dto.LessonId, dto.StudentIdLessons);
            return ok ? Ok("השיעור נקבע בהצלחה!")
                      : BadRequest("הזמנה נכשלה – בדוק שהשיעור פנוי ושנתוני התלמיד/מורה תקינים.");
        }


        //בקשה לקבל את כל השיעורים הממתינים של תלמיד
        //[HttpGet("getPendingLessons/{studentId}")]
        // public async Task<IActionResult> GetPendingLessons(int studentId)
        // {
        //     try
        //     {
        //         var lessons = await _studentManagerBL.GetStudentById(studentId); // קריאה למתודה ללא הקצאה
        //         return Ok(lessons); // החזרת השיעורים
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest($"Error: {ex.Message}");
        //     }
        // }
        // בקשה לקבל את כל השיעורים המושלמים של תלמיד
        //[HttpGet("getCompletedLessons/{studentId}")]
        // public async Task<IActionResult> GetCompletedLessons(int studentId)
        // {
        //     try
        //     {
        //         var lessons = await _studentManagerBL.GetCompletedLessons(studentId); // קריאה למתודה ללא הקצאה
        //         return Ok(lessons); // החזרת השיעורים
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest($"Error: {ex.Message}");
        //     }
        // }
    }
}
