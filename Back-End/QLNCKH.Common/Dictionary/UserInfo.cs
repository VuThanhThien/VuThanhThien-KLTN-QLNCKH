using QLNCKH.Common.IdentityApplication;
using System;

namespace QLNCKH.Common.Dictionary
{
    public class UserInfo
    {
        /// <summary>
        /// ID người dùng
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Mã người dùng
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Địa chỉ thường trú
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Phòng ban: 
        /// </summary>
        public int? Department { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        public int? Position { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string IdentityCode { get; set; }

        /// <summary>
        /// Địa chỉ công tác
        /// </summary>
        public string BusinessAddress { get; set; }

        /// <summary>
        /// thành tích 
        /// </summary>
        public string Achievements { get; set; }

        /// <summary>
        /// Giới tính createdby VTT 09/04/21
        /// </summary>
        public int Gender { get; set; }

        public UserInfo(Guid userID)
        {
            UserID = userID;
        }
    }
}
