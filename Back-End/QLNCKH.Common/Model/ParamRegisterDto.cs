
using System;

namespace QLNCKH.Common.Model
{
    public class ParamRegisterDto
    {
        /// <summary>
        /// tên đăng nhập
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// mật khẩu
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// xác nhận mật khẩu
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// tên đầy đủ
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Vị trí
        /// </summary>
        public int? Position { get; set; }

        /// <summary>
        /// mã nhân viên
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// Địa chỉ/ quê quán
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Phòng ban
        /// </summary>
        public int? Department { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// CMND/CCCD
        /// </summary>
        public string IdentityCode { get; set; }

        /// <summary>
        /// Địa chỉ coongv tác
        /// </summary>
        public string BusinessAddress { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Thành tích
        /// </summary>
        public string Achievements { get; set; }

        /// <summary>
        /// Bằng cấp
        /// </summary>
        public string Degree { get; set; }
    }
}
