
using System;

namespace QLNCKH.Common.Model
{
    public class ResponseAccountDto
    {
        /// <summary>
        /// ID 
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Thông báo
        /// </summary>
        public string Msg { get; set; }

    }
}
