using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.Common.NotificationCenter
{
    public class NotificationResponse
    {
        /// <summary>
        /// Mã
        /// </summary>
        public int Code { get; set; } = 200;

        /// <summary>
        /// Trạng thái thành công/ thất bại
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Thông báo
        /// </summary>
        public string Message { get; set; } = "Success";
    }
}
