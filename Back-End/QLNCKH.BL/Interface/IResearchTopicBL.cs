using QLNCKH.Common;
using QLNCKH.Common.Dictionary;

namespace QLNCKH.BL.Interface
{
    public interface IResearchTopicBL : IBaseBL<ResearchTopic>
    {
        /// <summary>
        /// Lấy thông tin đề tài bằng id người dùng
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        BaseResponse GetTopicByUserID(string userID);
    }
}
