using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.BL.Interface
{
    public interface IMemberTopicBL : IBaseBL<MemberTopic>
    {
        BaseResponse deleteMemberTopic(string UserID, string ResearchTopicID);
    }
}
