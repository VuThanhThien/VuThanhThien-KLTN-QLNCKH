﻿using Dapper;
using QLNCKH.Common.Dictionary;
using QLNCKH.DL.Base;
using QLNCKH.DL.Interface;
using System.Collections.Generic;

namespace QLNCKH.DL.Dictionary
{
    public class ResearchTopicDL : BaseDL<ResearchTopic>, IResearchTopicDL
    {
        public ResearchTopicDL(IDbContext<ResearchTopic> dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Lấy danh sách đề tài theo ID
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<ResearchTopic> GetTopicByUserID(object param)
        {
            var procName = "Proc_GetResearchTopicByUserID";

            var parameters = new DynamicParameters(param);

            var result = _dbContext.QueryStore(procName, parameters);

            return result;
        }
    }
}
