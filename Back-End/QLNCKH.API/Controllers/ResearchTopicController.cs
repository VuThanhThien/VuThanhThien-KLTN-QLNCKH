
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;
using System;

namespace QLNCKH.API.Controllers
{   
    /// <summary>
    /// API Đề tài
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResearchTopicController : BaseController<ResearchTopic>
    {
        /// <summary>
        /// Biến productBL gọi lên tầng Bussiness xử lý nghiệp vụ hàng hoá
        /// </summary>
        private readonly IResearchTopicBL _researchTopicBL;
        private readonly IMemberTopicBL _memberTopicBL;

        /// <summary>
        /// Controller đề tài kế thừa từ Base
        /// </summary>
        /// <param name="baseBL"></param>
        /// <param name="researchTopicBL"></param>
        /// <param name="memberTopicBL"></param>
        public ResearchTopicController(IBaseBL<ResearchTopic> baseBL, IResearchTopicBL researchTopicBL, IMemberTopicBL memberTopicBL) : base(baseBL)
        {
            _researchTopicBL = researchTopicBL;
            _memberTopicBL = memberTopicBL;
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="topic">Đối tượng cần thêm mới</param>
        /// <returns></returns>
        [HttpPost]
        public override IActionResult Post([FromBody] ResearchTopic topic)
        {
            var result = _researchTopicBL.Insert(topic);

            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        //[Authorize]
        //[HttpPut("{id}")]
        //public override IActionResult Put([FromRoute] Guid id, [FromBody] ResearchTopic topic)
        //{
        //    var result = _researchTopicBL.Insert(topic);

        //    return StatusCode((int)result.HTTPStatusCode, result.Data);
        //}

        /// <summary>
        /// Lấy danh sách đề tài theo userID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet("ByUserID/{userID}")]
        public IActionResult GetTopicByUserID(string userID)
        {
            var result = _researchTopicBL.GetTopicByUserID(userID);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        
        /// <summary>
        /// Lấy tất cả thành viên của topic này
        /// </summary>
        /// <returns></returns>
        /// created by vtthien 08.03.2021
        [HttpGet("{topicID}/members")]
        public virtual IActionResult GetAllMemberOfThisTopic([FromRoute] Guid topicID)
        {
            var result = _memberTopicBL.GetMemberTopicByTopicID(topicID);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }


    }
}
