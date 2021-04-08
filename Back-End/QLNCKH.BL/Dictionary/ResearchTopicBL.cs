using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using QLNCKH.Common.Enum;
using QLNCKH.DL.Interface;
using System;

namespace QLNCKH.BL.Dictionary
{
    public class ResearchTopicBL : BaseBL<ResearchTopic> , IResearchTopicBL
    {
        private readonly IResearchTopicDL _researchTopicDL;
        public ResearchTopicBL(IBaseDL<ResearchTopic> baseDL, IResearchTopicDL researchTopicDL) : base(baseDL)
        {
            _researchTopicDL = researchTopicDL;
        }

        public override BaseResponse Insert(ResearchTopic topic)
        {
            topic.ResearchID = Guid.NewGuid();
            // kiểm tra trùng mã SKU không
            var researchByCode = _baseDL.GetByCode(topic.ResearchCode);

            if(researchByCode != null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Bad_Request,
                    Data = "Đã trùng mã nghiên cứu"
                };
                return response;
            }
            var result = base.Insert(topic);
            result.Data = topic;
            return result;
        }

    }
}
