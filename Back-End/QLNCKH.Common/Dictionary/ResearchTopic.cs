﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.Common.Dictionary
{
    public class ResearchTopic
    {
        /// <summary>
        /// ID đề tài
        /// </summary>
        public Guid ResearchID { get; set; }

        /// <summary>
        /// Mã đề tài
        /// </summary>
        public string ResearchCode { get; set; }

        /// <summary>
        /// Tên đề tài
        /// </summary>
        public string ResearchName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Trạng thái(  1- hoàn thành nhiệm vụ, 2- không hoàn thành nhiệm vụ, 3- bị hủy)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Quá trình làm (1- đợi xét chọn, 2- đang làm, 3- đã hết hạn)
        /// </summary>
        public int Process { get; set; }

        /// <summary>
        /// ID tác giả
        /// </summary>
        public Guid UserID { get; set; }


        /// <summary>
        /// Tạo ngày
        /// </summary>
        public DateTime CreatedDate { get; set; }


        /// <summary>
        /// Tạo bởi
        /// </summary>
        public string CreatedBy { get; set; }


        /// <summary>
        /// Ngày thay đổi
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Chỉnh sửa bởi
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Kinh phí
        /// </summary>
        public decimal? Expense { get; set; }

        /// <summary>
        /// Ngày dự kiến kết thúc
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Ngày hết hạn
        /// </summary>
        public DateTime? ExpiredDate { get; set; }
    }
}