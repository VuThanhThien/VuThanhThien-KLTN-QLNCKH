using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;
using QLNCKH.DL.Interface;

namespace QLNCKH.BL.Dictionary
{
    public class ResearchTopicBL : BaseBL<ResearchTopic> , IResearchTopicBL
    {
        public ResearchTopicBL(IBaseDL<ResearchTopic> baseDL) : base(baseDL)
        {
        }
    }
}
