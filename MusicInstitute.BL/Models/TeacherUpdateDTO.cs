using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstitute.BL.Models
{
    // BL/Models/TeacherUpdateDTO.cs
        public class TeacherUpdateDTO
        {
            // רק השדות שהמשתמש יכול לערוך
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Phone { get; set; } = null!;
            public string Email { get; set; } = null!;
            public int ExperienceYears { get; set; }
        }
    
}
