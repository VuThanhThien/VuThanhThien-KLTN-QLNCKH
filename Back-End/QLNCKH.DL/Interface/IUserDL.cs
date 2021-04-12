using QLNCKH.Common.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.DL.Interface
{
    public interface IUserDL: IBaseDL<User>
    {
        public IEnumerable<User> CheckDuplicatedUser(object param);
    }
}
