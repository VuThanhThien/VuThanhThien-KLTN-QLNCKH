using QLNCKH.Common.Dictionary;
using QLNCKH.DL.Base;
using QLNCKH.DL.Interface;

namespace QLNCKH.DL.Dictionary
{
    public class UserDL : BaseDL<User>, IUserDL
    {
        public UserDL(IDbContext<User> dbContext) : base(dbContext)
        {
        }
    }
}
