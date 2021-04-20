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
        public int deleteMemberTopic(object param)
        {
            var procName = "Proc_DeleteMemberTopic";

            var parameters = new DynamicParameters(param);

            var result = _dbContext.ExcuteStore(procName, parameters);

            return result;
        }

        public IEnumerable<MemberTopic> GetMemberTopicByID(Guid id)
        {
            var storeName = "Proc_GetMemberTopicByID";


            var parameters = new
            {
                id = id.ToString()
            };

            var result = _dbContext.QueryStore(storeName, parameters);

            return result;
        }
    }
}
