namespace MusicInstitute.BL.Models
{
    public class StudentDTO
    {
        public int StudentId { get; set; }

        // דרושים בלוגין → נשארים non‑nullable
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string StudentPassword { get; set; } = null!;

        // לא חובה בלוגין → נסמן כ‑nullable
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Instrument { get; set; }

        // Level ממילא מספרי – נהפוך ל‑nullable כדי שלא יחויב
        public int? Level { get; set; }
    }
}
