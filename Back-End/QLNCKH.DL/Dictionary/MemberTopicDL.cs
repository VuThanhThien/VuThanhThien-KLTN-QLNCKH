using QLNCKH.Common.Dictionary;
using QLNCKH.DL.Base;
using QLNCKH.DL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.DL.Dictionary
{
    public class MemberTopicDL : BaseDL<MemberTopic> , IMemberTopicDL
    {
        public MemberTopicDL(IDbContext<MemberTopic> dbContext) : base(dbContext)
        {
        }

    }
}
