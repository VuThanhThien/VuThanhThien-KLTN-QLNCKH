using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.Common.NotificationCenter
{
    public class SlackAttach
    {
        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Có chia sắp xếp
        /// </summary>
        public bool IsShort { get; set; } = false;

        public SlackAttach(string title, string content, bool isShort = false)
        {
            Title = title;
            Content = content;
            IsShort = isShort;
        }
    }
}
