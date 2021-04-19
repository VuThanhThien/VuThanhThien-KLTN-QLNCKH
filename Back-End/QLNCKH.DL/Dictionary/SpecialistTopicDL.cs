using QLNCKH.Common.Dictionary;
using QLNCKH.DL.Base;
using QLNCKH.DL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.DL.Dictionary
{
    public class SpecialistTopicDL : BaseDL<SpecialistTopic> , ISpecialistTopicDL
    {
        public SpecialistTopicDL(IDbContext<SpecialistTopic> dbContext) : base(dbContext)
        {
        }
    }
}
