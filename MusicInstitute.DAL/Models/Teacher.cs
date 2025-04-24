using System;
using System.Collections.Generic;

namespace MusicInstitute.DAL.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int ExperienceYears { get; set; }

    public string TeacherPassword { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<Instrument> Instruments { get; set; } = new List<Instrument>();
}
