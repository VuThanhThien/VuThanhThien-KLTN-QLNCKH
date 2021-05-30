using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.Common.NotificationCenter
{
    public class Slack
    {
        /// <summary>
        /// Địa chỉ URL webhook
        /// </summary>
        public string WebHookUrl { get; private set; }

        /// <summary>
        /// Người gửi thông tin
        /// </summary>
        public string Author { get; private set; }

        /// <summary>
        /// Emoji
        /// </summary>
        public SlackEmoji Emoji { get; set; }

        /// <summary>
        /// Nội dung
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Danh sách các thông tin chi tiết
        /// </summary>
        public List<SlackAttach> SlackAttachs { get; set; }

        /// <summary>
        /// Màu sắc
        /// </summary>
        public SlackColor Color { get; set; }

        public Slack(string webHookUrl, string author, string content, List<SlackAttach> slackAttachs, SlackColor color = SlackColor.Default)
        {
            WebHookUrl = webHookUrl;
            Author = author;
            Content = content;
            SlackAttachs = slackAttachs;
            Color = color;
        }
    }
}
