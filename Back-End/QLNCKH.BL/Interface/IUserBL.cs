using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.BL.Interface
{
    public interface IUserBL : IBaseBL<User>
    {
        /// <summary>
        /// Kiểm tra trùng user
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="identityCode"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        BaseResponse CheckDuplicatedUser(string userCode, string identityCode, string phoneNumber);
    }
}
