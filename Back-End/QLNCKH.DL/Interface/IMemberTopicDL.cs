using QLNCKH.Common.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.DL.Interface
{
    public interface IMemberTopicDL : IBaseDL<MemberTopic>
    {
        public int deleteMemberTopic(object param);
        public IEnumerable<MemberTopic> GetMemberTopicByID(Guid id);
    }
}
