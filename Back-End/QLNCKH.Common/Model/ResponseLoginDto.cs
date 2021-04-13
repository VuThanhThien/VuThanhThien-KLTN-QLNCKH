using System;

namespace QLNCKH.Common.Model
{
    public class ResponseLoginDto
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// ID người dùng hiện tại
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Tên người dùng hiện tại
        /// createdby vtthien 130421
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Quyền của người hiện tại
        /// </summary>
        public string Role { get; set; }
    }
}
