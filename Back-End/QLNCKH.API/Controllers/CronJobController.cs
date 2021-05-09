using Microsoft.AspNetCore.Mvc;
using QLNCKH.BL.Interface;
using QLNCKH.BL.NotificationCenter;
using QLNCKH.Common;
using QLNCKH.Common.NotificationCenter;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace QLNCKH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CronJobController : ControllerBase
    {
        private readonly IResearchTopicBL _researchTopicBL;
        private readonly INotificationCenter _notificationCenter;

        public CronJobController(IResearchTopicBL researchTopicBL, INotificationCenter notificationCenter)
        {
            _researchTopicBL = researchTopicBL;
            _notificationCenter = notificationCenter;
        }

        /// <summary>
        /// API gửi email cho những đề tài còn {perday} ngày nữa là hết hạn
        /// </summary>
        /// <param name="perDay"></param>
        /// <returns></returns>
        [HttpPost("research-expire")]
        public virtual IActionResult ResearchExpire(int perDay)
        {
            // gọi lên db lấy thông tin
            var result = _researchTopicBL.GetAllEmailOfExpreTopic(perDay);
            
            if (result != null && result.Count > 0)
            {
                var file = Utility.ReadTemplateSendEmailExpire();
                foreach (var topic in result)
                {
                    var content = Utility.FormSendEmailExpire(file, new string[] { topic.Fullname, topic.ResearchName, topic.ExpiredDate?.ToString("dd/MM/yyyy") });
                    var message = new EmailMessage(
                                new string[] { topic.Email },
                                "[UET-QLNCKH] THÔNG BÁO NGHIỆM THU ĐỀ TÀI",
                                content,
                                null);
                    Task.Run(() => _notificationCenter.SendEmailAsync(message));
                }

                return Ok("Gửi thành công !");
            }

            return Ok("Không có đề tài nào sắp hết hạn");

        }
    }
}
