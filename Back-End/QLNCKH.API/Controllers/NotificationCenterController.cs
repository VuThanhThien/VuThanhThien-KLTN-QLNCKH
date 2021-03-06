using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLNCKH.BL.NotificationCenter;
using QLNCKH.Common.Model;
using QLNCKH.Common.NotificationCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLNCKH.API.Controllers
{
    /// <summary>
    /// Controller quản lý gửi email
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class NotificationCenterController : ControllerBase
    {
        private readonly INotificationCenter _notificationCenter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationCenter"></param>
        public NotificationCenterController(INotificationCenter notificationCenter)
        {
            _notificationCenter = notificationCenter;
        }

        /// <summary>
        /// Gửi mail ngay lập tức
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("send-email")]
        public virtual IActionResult SendEmail([FromBody] ParamSendEmail param)
        {

            var message = new EmailMessage(
                param.SendTo, 
                param.Title, 
                param.Content, 
                null);
            _notificationCenter.SendEmail(message);

            return Ok();
        }


        //[HttpPost("send-slack")]
        //public virtual IActionResult SendSlack()
        //{
        //    return Ok();
        //}
    }
}
