
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
    }
}
