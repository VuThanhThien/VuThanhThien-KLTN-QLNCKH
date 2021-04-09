using QLNCKH.Common;
using QLNCKH.Common.Dictionary;

namespace QLNCKH.BL.Interface
{
    public interface IResearchTopicBL : IBaseBL<ResearchTopic>
    {
        BaseResponse GetTopicByUserID(string userID);
    }
}
