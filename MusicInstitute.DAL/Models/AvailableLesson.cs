using System;
using System.Collections.Generic;

namespace MusicInstitute.DAL.Models;

public partial class AvailableLesson
{
    public int LessonId { get; set; }

    public DateOnly LessonDate { get; set; }

    public TimeOnly LessonTime { get; set; }

    public int Kind { get; set; }

    public int TeacherIdLessons { get; set; }

    public int DurationMinutes { get; set; }

    public virtual Teacher TeacherIdLessonsNavigation { get; set; } = null!;
}
