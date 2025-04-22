using System;
using System.Collections.Generic;

namespace MusicInstitute.DAL.Models;

public partial class GroupLesson
{
    public int GroupLessonId { get; set; }

    public string Title { get; set; } = null!;

    public int TeacherIdGroupLessons { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int DurationMinutes { get; set; }

    public virtual Teacher TeacherIdGroupLessonsNavigation { get; set; } = null!;

    public virtual ICollection<Student> StudentIdGroupLessonStudents { get; set; } = new List<Student>();
}
