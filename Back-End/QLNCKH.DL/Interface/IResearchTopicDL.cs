using QLNCKH.Common.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.DL.Interface
{
    public interface IResearchTopicDL : IBaseDL<ResearchTopic>
    {
        /// <summary>
        /// Lấy đề tài theo id tác giả
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<ResearchTopic> GetTopicByUserID(object param);

        /// <summary>
        /// Hàm lấy thông tin những đề tài sắp hết hạn và chủ nhiệm đề tài đó
        /// </summary>
        /// <param name="perDay"></param>
        /// <returns></returns>
        List<ResearchTopic> GetAllEmailOfExpreTopic (int perDay);
    }
}
