using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using QLNCKH.DL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.BL.Dictionary
{
    class AccountBL : BaseBL<Account>, IAccountBL
    {
        public AccountBL(IBaseDL<Account> baseDL) : base(baseDL)
        {
        }
    }
}
