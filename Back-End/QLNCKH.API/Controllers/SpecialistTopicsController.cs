using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLNCKH.API.Controllers
{
    /// <summary>
    /// Controller cán bộ đánh giá
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistTopicsController : BaseController<SpecialistTopic>
    {
        private readonly ISpecialistTopicBL _specialistTopicBL;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="baseBL"></param>
       /// <param name="specialistTopicBL"></param>
        public SpecialistTopicsController(IBaseBL<SpecialistTopic> baseBL, ISpecialistTopicBL specialistTopicBL) : base(baseBL)
        {
            _specialistTopicBL = specialistTopicBL;
        }
    }
}
