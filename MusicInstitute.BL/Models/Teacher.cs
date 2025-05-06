using System;
using System.Collections.Generic;

namespace MusicInstitute.BL.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int ExperienceYears { get; set; }

    public string TeacherPassword { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<AvailableLesson> AvailableLessons { get; set; } = new List<AvailableLesson>();

    public virtual ICollection<BookedLesson> BookedLessons { get; set; } = new List<BookedLesson>();

    public virtual ICollection<PassedLesson> PassedLessons { get; set; } = new List<PassedLesson>();

    public virtual ICollection<Instrument> Instruments { get; set; } = new List<Instrument>();
}
