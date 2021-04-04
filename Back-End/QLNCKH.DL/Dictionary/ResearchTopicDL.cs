using QLNCKH.Common.Dictionary;
using QLNCKH.DL.Base;
using QLNCKH.DL.Interface;

namespace QLNCKH.DL.Dictionary
{
    public class ResearchTopicDL : BaseDL<ResearchTopic>, IResearchTopicDL
    {
        public ResearchTopicDL(IDbContext<ResearchTopic> dbContext) : base(dbContext)
        {
        }
    }
}
