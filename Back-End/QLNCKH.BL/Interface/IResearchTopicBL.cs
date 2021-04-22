using Microsoft.AspNetCore.Http;
using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using System.Threading.Tasks;

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
        string GenFileName(IFormFile file);
        Task<bool> WriteFile(IFormFile file, string fileName);
    }
}
