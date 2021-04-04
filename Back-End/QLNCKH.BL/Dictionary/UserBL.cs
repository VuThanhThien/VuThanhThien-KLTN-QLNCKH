using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;
using QLNCKH.DL.Interface;

namespace QLNCKH.BL.Dictionary
{
    public class UserBL : BaseBL<User>, IUserBL
    {
        public UserBL(IBaseDL<User> baseDL) : base(baseDL)
        {
        }

        private readonly IUserDL _userDL;

    }
}
