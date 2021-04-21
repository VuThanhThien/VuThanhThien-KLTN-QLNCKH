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

        /// <summary>
        /// API get thành viên theo id đề tài
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ByTopicID/{id}")]
        public  IActionResult GetByTopicID( Guid id)
        {
            var result = _memberTopicBL.GetMemberTopicByTopicID(id);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }
    }
}
