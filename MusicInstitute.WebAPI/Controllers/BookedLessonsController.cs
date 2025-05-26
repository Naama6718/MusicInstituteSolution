using Microsoft.AspNetCore.Mvc;
using MusicInstitute.BL.Api;
using MusicInstitute.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicInstitute.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookedLessonsController : ControllerBase
    {
        private readonly IBookedLessons_Manager_BL _bookedLessonsManager;

        public BookedLessonsController(IBookedLessons_Manager_BL bookedLessonsManager)
        {
            _bookedLessonsManager = bookedLessonsManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookedLessonDTO>>> GetAllBookedLessons()
        {
            var lessons = await _bookedLessonsManager.GetAllBookedLessons();
            return Ok(lessons);
        }

        [HttpGet("teacher/{teacherName}")]
        public async Task<ActionResult<List<BookedLessonDTO>>> GetLessonsByTeacher(string teacherName)
        {
            var lessons = await _bookedLessonsManager.GetLessonsByTeacher(teacherName);
            return Ok(lessons);
        }

        [HttpGet("instrument/{instrumentName}")]
        public async Task<ActionResult<List<BookedLessonDTO>>> GetLessonsByInstrument(string instrumentName)
        {
            var lessons = await _bookedLessonsManager.GetLessonsByInstrument(instrumentName);
            return Ok(lessons);
        }

        [HttpGet("student/{studentName}")]
        public async Task<ActionResult<List<BookedLessonDTO>>> GetLessonsByStudent(string studentName)
        {
            var lessons = await _bookedLessonsManager.GetLessonsByStudent(studentName);
            return Ok(lessons);
        }

        [HttpGet("{lessonId}")]
        public async Task<ActionResult<BookedLessonDTO>> GetLessonById(int lessonId)
        {
            var lesson = await _bookedLessonsManager.GetLessonById(lessonId);
            if (lesson == null)
                return NotFound();

            return Ok(lesson);
        }

        [HttpPost]
        public async Task<ActionResult> AddLesson([FromBody] BookedLessonDTO lessonDto)
        {
            await _bookedLessonsManager.AddLesson(lessonDto);
            return CreatedAtAction(nameof(GetLessonById), new { lessonId = lessonDto.LessonId }, lessonDto);
        }

        [HttpDelete("{lessonId}")]
        public async Task<ActionResult> RemoveLesson(int lessonId)
        {
            var removed = await _bookedLessonsManager.RemoveLesson(lessonId);
            if (!removed)
                return NotFound();

            return NoContent();
        }

        // הפעולה להעברת שיעור לשיעורים שעברו
        //[HttpPost("moveToPassed/{bookedLessonId}")]
        //public async Task<ActionResult> MoveToPassedLesson(int bookedLessonId)
        //{
        //    var moved = await _bookedLessonsManager.MoveToPassedLesson(bookedLessonId);
        //    if (!moved)
        //        return NotFound();

        //    return Ok();
        //}
    }
}
