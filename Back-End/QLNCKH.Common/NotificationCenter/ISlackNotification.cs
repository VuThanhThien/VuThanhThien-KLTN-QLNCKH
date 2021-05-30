using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QLNCKH.Common.NotificationCenter
{
    public interface ISlackNotification
    {
        Task<NotificationResponse> SendSlackNotification(string author = SlackAuthor.Default, SlackEmoji emoji = SlackEmoji.Slack, string content = null, SlackColor color = SlackColor.Default, List<SlackAttach> slackAttachs = null);

        Task<NotificationResponse> SendSlackNotification(string content);

        Task<NotificationResponse> SendSlackNotification(Slack slack);
    }
}
