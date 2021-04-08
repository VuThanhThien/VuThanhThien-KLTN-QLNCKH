
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;

namespace QLNCKH.API.Controllers
{   
    /// <summary>
    /// API Đề tài
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ResearchTopicController : BaseController<ResearchTopic>
    {
        /// <summary>
        /// Biến productBL gọi lên tầng Bussiness xử lý nghiệp vụ hàng hoá
        /// </summary>
        private readonly IResearchTopicBL _researchTopicBL;

        /// <summary>
        /// Controller đề tài kế thừa từ Base
        /// </summary>
        /// <param name="baseBL"></param>
        /// <param name="researchTopicBL"></param>
        public ResearchTopicController(IBaseBL<ResearchTopic> baseBL, IResearchTopicBL researchTopicBL) : base(baseBL)
        {
            _researchTopicBL = researchTopicBL;
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="topic">Đối tượng cần thêm mới</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public override IActionResult Post([FromBody] ResearchTopic topic)
        {
            var result = _researchTopicBL.Insert(topic);

            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }
    }
}
