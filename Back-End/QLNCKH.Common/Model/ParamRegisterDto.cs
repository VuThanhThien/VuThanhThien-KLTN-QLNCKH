
using System;

namespace QLNCKH.Common.Model
{
    public class ParamRegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? Position { get; set; }
        public string UserCode { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Department { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityCode { get; set; }
        public string BusinessAddress { get; set; }
    }
}
