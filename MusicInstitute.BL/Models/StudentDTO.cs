﻿namespace MusicInstitute.BL.Models
{
    public class StudentDTO
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Instrument { get; set; } = null!;

        public int Level { get; set; }
        public string StudentPassword { get; set; } = null!;

    }
}