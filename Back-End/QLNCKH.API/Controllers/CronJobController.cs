using Microsoft.AspNetCore.Mvc;
using QLNCKH.BL.Interface;
using QLNCKH.BL.NotificationCenter;
using QLNCKH.Common.NotificationCenter;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost("research-expire")]
        public virtual IActionResult ResearchExpire(int perDay)
        {
            // gọi lên db lấy thông tin
            var result = _researchTopicBL.GetAllEmailOfExpreTopic(perDay);
            
            if (result != null && result.Count > 0)
            {
                
                foreach(var topic in result)
                {
                    var message = new EmailMessage(
                                new string[] { topic.Email },
                                "[UET-QLNCKH] THÔNG BÁO NGHIỆM THU ĐỀ TÀI",
                                "Phòng hoa học Công nghệ & Hợp tác Phát triển thống báo, đề tài"
                                + topic.ResearchName + "của chủ nhiệm "
                                + topic.Fullname
                                + " sắp hết hạn vào ngày "
                                + topic.ExpiredDate+". " 
                                +"Vui lòng chuẩn bị để nghiệm thu. Thông tin chi tiết vui lòng liên hệ Phòng KHCN&HTPT.",
                                null);

                    Task.Run(() => _notificationCenter.SendEmailAsync(message));
                }

                return Ok("Gửi thành công !");
            }

            return Ok("Không có đề tài nào sắp hết hạn");

        }
    }
}
