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
        public IEnumerable<ResearchTopic> GetTopicByUserID(object param);
    }
}
