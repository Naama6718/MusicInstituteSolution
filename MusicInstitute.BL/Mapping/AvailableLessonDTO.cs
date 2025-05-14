namespace MusicInstitute.BL.Mapping
{
    public class AvailableLessonDTO
    {
        public int LessonId { get; set; }
        public DateOnly LessonDate { get; set; }
        public TimeOnly LessonTime { get; set; }
        public string Kind { get; set; } = null!;
        public int TeacherIdLessons { get; set; }
        public int DurationMinutes { get; set; }

        // אם תצטרך את שם המורה או פרטים אחרים על המורה, אפשר להוסיף אותם
        public string TeacherFirstName { get; set; } = null!;
        public string TeacherLastName { get; set; } = null!;
    }
}
