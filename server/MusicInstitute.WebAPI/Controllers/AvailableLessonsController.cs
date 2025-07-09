using Microsoft.AspNetCore.Mvc;
using MusicInstitute.BL.Models;
using MusicInstitute.BL.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicInstitute.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailableLessonsController : ControllerBase
    {
        private readonly AvailableLessons_Manager_BL _availableLessonsManager;

        public AvailableLessonsController(AvailableLessons_Manager_BL availableLessonsManager)
        {
            _availableLessonsManager = availableLessonsManager;
        }

        [HttpGet("by-teacher/{teacherName}")]
        public async Task<ActionResult<List<AvailableLessonDTO>>> GetLessonsByTeacher(string teacherName)
        {
            var lessons = await _availableLessonsManager.GetLessonsByTeacher(teacherName);
            if (lessons == null || lessons.Count == 0)
                return NotFound($"No lessons found for teacher: {teacherName}");

            return Ok(lessons);
        }

        [HttpGet("by-instrument/{instrumentName}")]
        public async Task<ActionResult<List<AvailableLessonDTO>>> GetLessonsByInstrument(string instrumentName)
        {
            var lessons = await _availableLessonsManager.GetLessonsByInstrument(instrumentName);
            if (lessons == null || lessons.Count == 0)
                return NotFound($"No lessons found for instrument: {instrumentName}");

            return Ok(lessons);
        }

        [HttpGet("by-date/{date}")]
        public async Task<ActionResult<List<AvailableLessonDTO>>> GetLessonsByDate(string date)
        {
            if (!DateOnly.TryParse(date, out var parsedDate))
                return BadRequest("Invalid date format. Use yyyy-MM-dd");

            var lessons = await _availableLessonsManager.GetLessonsByDate(parsedDate);
            if (lessons == null || lessons.Count == 0)
                return NotFound($"No lessons found for date: {date}");

            return Ok(lessons);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<AvailableLessonDTO>>> GetAllLessons()
        {
            var lessons = await _availableLessonsManager.GetAllLessons();
            return Ok(lessons);
        }

        [HttpPost("book")]
        public async Task<ActionResult> BookLesson([FromQuery] int lessonId, [FromQuery] int studentId)
        {
            var success = await _availableLessonsManager.BookSelectedLessonAsync(lessonId, studentId);
            if (!success)
                return BadRequest("Failed to book the lesson. It might be already booked or not exist.");

            return Ok("Lesson booked successfully.");
        }

        // הוספת שיעור חדש
        [HttpPost("add")]
        public async Task<ActionResult> AddLesson([FromBody] AvailableLessonDTO lessonDto)
        {
            if (lessonDto == null)
                return BadRequest("Lesson data is null.");

            await _availableLessonsManager.AddLesson(lessonDto);
            return Ok("Lesson added successfully.");
        }

        //// עדכון שיעור קיים
        //[HttpPut("update/{lessonId}")]
        //public async Task<ActionResult> UpdateLesson(int lessonId, [FromBody] AvailableLessonDTO lessonDto)
        //{
        //    if (lessonDto == null)
        //        return BadRequest("Lesson data is null.");

        //    // אפשר להוסיף כאן בדיקה שה-id בשיעורDto תואם ל-lessonId אם יש מזהה ב-DTO

        //    var existingLessons = await _availableLessonsManager.GetAllLessons();
        //    var lessonToUpdate = existingLessons.Find(l => l.LessonId == lessonId);
        //    if (lessonToUpdate == null)
        //        return NotFound("Lesson not found.");

        //    // עדכון השיעור - אם במנהל ה-BL אין פונקציה לעדכון, תצטרך להוסיף אותה.
        //    // כאן נניח שיש פונקציה בשם UpdateLesson ב-AvailableLessons_Manager_BL

        //    // מחליף את הנתונים ב-lessonDto כולל מזהה השיעור
        //    lessonDto.LessonId = lessonId;

        //    var updated = await _availableLessonsManager.UpdateLesson(lessonDto);
        //    if (!updated)
        //        return BadRequest("Failed to update the lesson.");

        //    return Ok("Lesson updated successfully.");
        //}

        // מחיקת שיעור
        [HttpDelete("delete/{lessonId}")]
        public async Task<ActionResult> DeleteLesson(int lessonId)
        {
            var success = await _availableLessonsManager.RemoveLesson(lessonId);
            if (!success)
                return NotFound("Lesson not found or could not be deleted.");

            return Ok("Lesson deleted successfully.");
        }
    }
}
