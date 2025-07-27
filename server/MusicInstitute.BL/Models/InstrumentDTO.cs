using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstitute.BL.Models
{
    public class InstrumentDTO
    {

        public int InstrumentId { get; set; }
        public string Name { get; set; } = null!; // <-- שינוי השם כאן
        public List<int> TeacherIds { get; set; } = new List<int>();
    }
}

