using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstitute.BL.Models
{
    public class PasswordResetRequest
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public string NewPassword { get; set; }      // הסיסמה החדשה

        public DateTime Expiration { get; set; }

    }
}
