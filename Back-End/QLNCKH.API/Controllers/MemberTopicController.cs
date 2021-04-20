using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;
using QLNCKH.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLNCKH.API.Controllers
{
    /// <summary>
    /// Thành viên tham gia nghiên cứu
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MemberTopicController : BaseController<MemberTopic>
    {
        private readonly IMemberTopicBL _memberTopicBL;
        /// <summary>
        /// Kế thừa lại base
        /// </summary>
        /// <param name="baseBL"></param>
        /// <param name="memberTopicBL"></param>
        public MemberTopicController(IBaseBL<MemberTopic> baseBL, IMemberTopicBL memberTopicBL) : base(baseBL)
        {
            _memberTopicBL = memberTopicBL;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteMemberTopic")]
        public  IActionResult DeleteMemberTopic([FromBody] ParamMemberTopicDto paramMemberTopicDto)
        {
            string UserID = paramMemberTopicDto.UserID.ToString();
            string ResearchTopicID = paramMemberTopicDto.ResearchTopicID.ToString();
            var result = _memberTopicBL.deleteMemberTopic( UserID,  ResearchTopicID);

            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }
    }
}
