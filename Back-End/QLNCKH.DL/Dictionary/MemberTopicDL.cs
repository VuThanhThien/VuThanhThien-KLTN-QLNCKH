using Dapper;
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

        public IEnumerable<MemberTopic> GetMemberTopicByMemberID(Guid memberId)
        {
            var storeName = "Proc_GetMemberTopicByMemberID";


            var parameters = new
            {
                userID = memberId.ToString()
            };

            var result = _dbContext.QueryStore(storeName, parameters);

            return result;
        }

        public IEnumerable<MemberTopic> GetMemberTopicByTopicID(Guid id)
        {
            var storeName = "Proc_GetMemberTopicByTopicID";


            var parameters = new
            {
                id = id.ToString()
            };

            var result = _dbContext.QueryStore(storeName, parameters);

            return result;
        }
    }
}
