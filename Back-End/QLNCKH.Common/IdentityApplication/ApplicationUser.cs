using Microsoft.AspNetCore.Identity;

namespace QLNCKH.Common.IdentityApplication
{
    public class ApplicationUser : IdentityUser
    {

        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; }
    }
}
