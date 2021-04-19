
using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;
using QLNCKH.DL.Dictionary;
using QLNCKH.DL.Interface;

namespace QLNCKH.BL.Dictionary
{
    public class MemberTopicBL : BaseBL<MemberTopic> , IMemberTopicBL
    {
        private readonly IMemberTopicDL _memberTopicDL;
        public MemberTopicBL(IBaseDL<MemberTopic> baseDL, IMemberTopicDL memberTopicDL) : base(baseDL)
        {
            _memberTopicDL = memberTopicDL;
        }
    }
}
