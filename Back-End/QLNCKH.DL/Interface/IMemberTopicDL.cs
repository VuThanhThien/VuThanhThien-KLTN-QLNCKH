using QLNCKH.Common.Dictionary;
using System;
using System.Collections.Generic;

namespace QLNCKH.DL.Interface
{
    public interface IMemberTopicDL : IBaseDL<MemberTopic>
    {
        /// <summary>
        /// Từ topicId => lấy danh sách thành viên của topic này
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public IEnumerable<MemberTopic> GetMemberTopicByTopicID(Guid topicId);

        /// <summary>
        /// Từ memberId => lấy danh sách topic mà member này là thành viên
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<MemberTopic> GetMemberTopicByMemberID(Guid memberId);
    }
}
