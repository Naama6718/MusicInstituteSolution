using System;
using System.Collections.Generic;

namespace MusicInstitute.BL.Models;

public partial class Instrument
{
    public int InstrumentId { get; set; }

    public string LessonName { get; set; } = null!;

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
