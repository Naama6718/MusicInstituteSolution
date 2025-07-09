namespace MusicInstitute.BL.Models
{
    public class PassedLessonDTO
    {
        public int LessonId { get; set; }
        public DateOnly LessonDate { get; set; }
        public TimeOnly LessonTime { get; set; }
        public string Kind { get; set; } = null!;
        public int StudentIdLessons { get; set; }
        public int TeacherIdLessons { get; set; }
        public int DurationMinutes { get; set; }

        // פרטי תלמיד (אם רוצים למפות את המידע של התלמיד)
        public string StudentFirstName { get; set; } = null!;
        public string StudentLastName { get; set; } = null!;

        // פרטי מורה (אם רוצים למפות את המידע של המורה)
        public string TeacherFirstName { get; set; } = null!;
        public string TeacherLastName { get; set; } = null!;
    }
}
