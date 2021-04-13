using QLNCKH.Common.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.DL.Interface
{
    public interface IUserDL: IBaseDL<User>
    {
        /// <summary>
        /// Check trùng
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<User> CheckDuplicatedUser(object param);
    }
}
