using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.BL.Interface
{
    public interface IUserBL : IBaseBL<User>
    {
        BaseResponse CheckDuplicatedUser(string userCode, string identityCode, string phoneNumber);
    }
}
