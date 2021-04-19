using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.Common.Dictionary
{
    /// <summary>
    /// model thành viên tham gia
    /// </summary>
    public class MemberTopic
    {
        /// <summary>
        /// ID thành viên
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// id đề tài
        /// </summary>
        public Guid ResearchTopicID { get; set; }

        /// <summary>
        /// vai trò trong đề tài
        /// </summary>
        public string ResearchRole { get; set; }

        /// <summary>
        /// Số tháng làm việc
        /// </summary>
        public int Workmonth { get; set; }
    }
}
