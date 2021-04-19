using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.Common.Dictionary
{
    /// <summary>
    /// Model cán bộ đánh giá
    /// </summary>
    public class SpecialistTopic
    {
        /// <summary>
        /// ID cán bộ
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// ID đề tài
        /// </summary>
        public Guid ResearchTopicID { get; set; }
    }
}
