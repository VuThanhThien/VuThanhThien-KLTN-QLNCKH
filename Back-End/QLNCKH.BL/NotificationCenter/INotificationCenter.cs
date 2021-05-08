
using QLNCKH.Common.NotificationCenter;
using System.Threading.Tasks;

namespace QLNCKH.BL.NotificationCenter
{
    /// <summary>
    /// Interface khai báo các hàm gửi thông báo
    /// </summary>
    public interface INotificationCenter
    {
        /// <summary>
        /// Thực hiện gửi email
        /// </summary>
        /// <param name="message">Thông tin email</param>
        void SendEmail(EmailMessage message);

        /// <summary>
        /// Thực hiện gửi email
        /// </summary>
        /// <param name="message">Thông tin email</param>
        /// <returns></returns>
        Task SendEmailAsync(EmailMessage message);
    }
}
