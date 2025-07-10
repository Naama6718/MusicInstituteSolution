namespace MusicInstitute.BL.Models
{
    public class TeacherDTO
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int ExperienceYears { get; set; }
        public string Email { get; set; } = null!;
        public string TeacherPassword { get; set; } = null!;

        public List<InstrumentDTO> Instruments { get; set; } = new List<InstrumentDTO>();
        public List<AvailableLessonDTO> AvailableLessons { get; set; } = new List<AvailableLessonDTO>();
        public List<BookedLessonDTO> BookedLessons { get; set; } = new List<BookedLessonDTO>();
        public List<PassedLessonDTO> PassedLessons { get; set; } = new List<PassedLessonDTO>();
    }
}
