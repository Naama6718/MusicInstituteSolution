using Microsoft.AspNetCore.Mvc;
using MusicInstitute.BL.Api;
using MusicInstitute.BL.Models;
using System;
using System.Threading.Tasks;

namespace MusicInstitute.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacher_Manager_BL _teacherManagerBL;

        public TeachersController(ITeacher_Manager_BL teacherManagerBL)
        {
            _teacherManagerBL = teacherManagerBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacherManagerBL.GetAllTeachers();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _teacherManagerBL.GetTeacherByIdAsync(id);
            return Ok(teacher);
        }

        [HttpGet("by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var teacher = await _teacherManagerBL.GetTeacherByEmailAsync(email);
            return Ok(teacher);
        }

        [HttpGet("by-login")]
        public async Task<IActionResult> GetByLogin([FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string password)
        {
            var teacher = await _teacherManagerBL.GetTeacherByNameAndPasswordAsync(firstName, lastName, password);
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TeacherDTO teacherDTO)
        {
            await _teacherManagerBL.AddTeacher(teacherDTO);
            return Ok("Teacher added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] string currentPassword, [FromBody] TeacherDTO teacherDTO)
        {
            await _teacherManagerBL.UpdateTeacherById(id, currentPassword, teacherDTO);
            return Ok("Teacher updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teacherManagerBL.DeleteTeacherAsync(id);
            return Ok("Teacher deleted successfully.");
        }

        [HttpPost("password-reset/request")]
        public async Task<IActionResult> RequestPasswordReset([FromQuery] string email)
        {
            await _teacherManagerBL.RequestPasswordResetAsync(email);
            return Ok("Password reset code sent to email.");
        }

        [HttpPost("password-reset/confirm")]
        public async Task<IActionResult> ConfirmPasswordReset([FromQuery] string email, [FromQuery] string code, [FromQuery] string newPassword)
        {
            await _teacherManagerBL.ConfirmPasswordResetAsync(email, code, newPassword);
            return Ok("Password reset successfully.");
        }
    }
}