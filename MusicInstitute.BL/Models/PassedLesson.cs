using System;
using System.Collections.Generic;

namespace MusicInstitute.BL.Models;

public partial class PassedLesson
{
    public int LessonId { get; set; }

    public DateOnly LessonDate { get; set; }

    public TimeOnly LessonTime { get; set; }

    public string Kind { get; set; } = null!;

    public int StudentIdLessons { get; set; }

    public int TeacherIdLessons { get; set; }

    public int DurationMinutes { get; set; }

    public virtual Student StudentIdLessonsNavigation { get; set; } = null!;

    public virtual Teacher TeacherIdLessonsNavigation { get; set; } = null!;
}
