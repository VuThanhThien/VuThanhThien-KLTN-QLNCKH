using SlackBotMessages;
using SlackBotMessages.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLNCKH.Common.NotificationCenter
{
    public class SlackNotification: ISlackNotification
    {
        private readonly SbmClient _slackClient;

        public SlackNotification()
        {
            _slackClient = new SbmClient(SlackConstant.DefaultURL);
        }

        public async Task<NotificationResponse> SendSlackNotification(string author = SlackAuthor.Default, SlackEmoji emoji = SlackEmoji.Slack, string content = null, SlackColor color = SlackColor.Default, List<SlackAttach> slackAttachs = null)
        {
            var result = new NotificationResponse();
            try
            {
                var message = new Message(content).SetUserWithEmoji(author, ConvertEmoji(emoji));

                if (slackAttachs != null && slackAttachs.Count > 0)
                {
                    var attachment = new Attachment();
                    foreach (var attach in slackAttachs)
                    {
                        attachment.AddField(attach.Title, attach.Content, attach.IsShort);
                    }
                    attachment.SetColor(ConvertColor(color));
                    message.AddAttachment(attachment);
                }
                var resultSend = await _slackClient.SendAsync(message);
                result.Message = resultSend;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<NotificationResponse> SendSlackNotification(string content)
        {
            var result = new NotificationResponse();
            try
            {
                var message = new Message(content).SetUserWithEmoji(SlackAuthor.Default, ConvertEmoji());

                var resultSend = await _slackClient.SendAsync(message);

                result.Message = resultSend;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<NotificationResponse> SendSlackNotification(Slack slack)
        {
            var result = new NotificationResponse();
            try
            {
                var message = new Message(slack.Content).SetUserWithEmoji(slack.Author, ConvertEmoji(slack.Emoji));

                if (slack.SlackAttachs != null && slack.SlackAttachs.Count > 0)
                {
                    var attachment = new Attachment();
                    foreach (var attach in slack.SlackAttachs)
                    {
                        attachment.AddField(attach.Title, attach.Content, attach.IsShort);
                    }
                    attachment.SetColor(ConvertColor(slack.Color));
                    message.AddAttachment(attachment);
                }

                var resultSend = string.IsNullOrEmpty(slack.WebHookUrl) ?
                    await _slackClient.SendAsync(message)
                    : await new SbmClient(slack.WebHookUrl).SendAsync(message);

                result.Message = resultSend;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        private string ConvertColor(SlackColor color = SlackColor.Default)
        {
            switch (color)
            {
                default:
                    return SlackColorValue.Default;
            }
        }

        private string ConvertEmoji(SlackEmoji emoji = SlackEmoji.Slack)
        {
            switch (emoji)
            {
                default:
                    return SlackEmojiValue.Slack;
            }
        }
    }
}
