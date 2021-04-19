using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;
using QLNCKH.DL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.BL.Dictionary
{
    public class SpecialistTopicBL : BaseBL<SpecialistTopic> , ISpecialistTopicBL
    {
        private readonly ISpecialistTopicDL _specialistTopicDL;
        public SpecialistTopicBL(IBaseDL<SpecialistTopic> baseDL, ISpecialistTopicDL  specialistTopicDL) : base(baseDL)
        {
            _specialistTopicDL = specialistTopicDL;
        }
    }
}
