using Dapper;
using QLNCKH.Common.Dictionary;
using QLNCKH.DL.Base;
using QLNCKH.DL.Interface;
using System.Collections.Generic;

namespace QLNCKH.DL.Dictionary
{
    public class UserDL : BaseDL<User>, IUserDL
    {
        public UserDL(IDbContext<User> dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Check trùng user
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<User> CheckDuplicatedUser(object param)
        {
            var procName = "Proc_CheckDuplicatedUser";

            var parameters = new DynamicParameters(param);

            var result = _dbContext.QueryStore(procName, parameters);

            return result;
        }
    }
}
