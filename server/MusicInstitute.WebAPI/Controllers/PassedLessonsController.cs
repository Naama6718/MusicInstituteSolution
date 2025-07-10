using Microsoft.AspNetCore.Mvc;
using MusicInstitute.BL.Api;
using MusicInstitute.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicInstitute.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassedLessonsController : ControllerBase
    {
        private readonly IPassedLessons_Manager_BL _passedLessonsManager;

        public PassedLessonsController(IPassedLessons_Manager_BL passedLessonsManager)
        {
            _passedLessonsManager = passedLessonsManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<PassedLessonDTO>>> GetAll()
        {
            var lessons = await _passedLessonsManager.GetAllLessons();
            return Ok(lessons);
        }

        [HttpGet("teacher/{teacherName}")]
        public async Task<ActionResult<List<PassedLessonDTO>>> GetByTeacher(string teacherName)
        {
            var lessons = await _passedLessonsManager.GetLessonsByTeacher(teacherName);
            return Ok(lessons);
        }

        [HttpGet("instrument/{instrumentName}")]
        public async Task<ActionResult<List<PassedLessonDTO>>> GetByInstrument(string instrumentName)
        {
            var lessons = await _passedLessonsManager.GetLessonsByInstrument(instrumentName);
            return Ok(lessons);
        }

        [HttpGet("student/{studentName}")]
        public async Task<ActionResult<List<PassedLessonDTO>>> GetByStudent(string studentName)
        {
            var lessons = await _passedLessonsManager.GetLessonsByStudent(studentName);
            return Ok(lessons);
        }

        [HttpPost]
        public async Task<ActionResult> AddLesson([FromBody] PassedLessonDTO lessonDto)
        {
            await _passedLessonsManager.AddLesson(lessonDto);
            return CreatedAtAction(nameof(GetAll), null);
        }
    }
}
