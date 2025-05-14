namespace MusicInstitute.BL.Mapping
{
    public class TeacherDTO
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int ExperienceYears { get; set; }
        public string Email { get; set; } = null!;

        // אם תרצה לכלול את השיעורים הזמינים והנגינה (Instruments) 
        public List<string> AvailableLessons { get; set; } = new List<string>();
        public List<string> Instruments { get; set; } = new List<string>();

        // אם לא צריך לשמור את הקולקציות של שיעורים (BookedLessons, PassedLessons), אפשר להשמיט
        // public List<BookedLessonDTO> BookedLessons { get; set; } = new List<BookedLessonDTO>();
        // public List<PassedLessonDTO> PassedLessons { get; set; } = new List<PassedLessonDTO>();
    }
}
