using QLNCKH.Common.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.DL.Interface
{
    public interface IResearchTopicDL : IBaseDL<ResearchTopic>
    {
        public IEnumerable<ResearchTopic> GetTopicByUserID(object param);
    }
}
