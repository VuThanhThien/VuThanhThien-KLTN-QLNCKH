using System;

namespace QLNCKH.Common.Model
{
    public class ResponseLoginDto
    {
        public string Token { get; set; }

        public Guid UserID { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }
    }
}
