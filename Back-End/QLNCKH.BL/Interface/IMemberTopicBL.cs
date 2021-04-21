using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.BL.Interface
{
    public interface IMemberTopicBL : IBaseBL<MemberTopic>
    {
        /// <summary>
        /// Từ topicId => lấy danh sách thành viên của topic này
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        BaseResponse GetMemberTopicByTopicID(Guid topicId);

        /// <summary>
        /// Từ memberId => lấy danh sách topic mà member này là thành viên
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        BaseResponse GetMemberTopicByMemberID(Guid memberId);
    }
}
