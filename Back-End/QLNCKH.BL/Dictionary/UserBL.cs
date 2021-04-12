using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common;
using QLNCKH.Common.Dictionary;
using QLNCKH.Common.Enum;
using QLNCKH.DL.Interface;

namespace QLNCKH.BL.Dictionary
{
    public class UserBL : BaseBL<User>, IUserBL
    {
        private readonly IUserDL _userDL;
        public UserBL(IBaseDL<User> baseDL, IUserDL userDL) : base(baseDL)
        {
            _userDL = userDL;
        }

        public BaseResponse CheckDuplicatedUser(string userCode, string identityCode, string phoneNumber)
        {
            var param = new
            {
                userCode = userCode,
                identityCode = identityCode,
                phoneNumber = phoneNumber
            };
            var result = _userDL.CheckDuplicatedUser(param);
            // danh sách rỗng => trả vè mã 204
            if (result == null)
            {
                // khởi tạo dữ liệu trả về
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.No_ConTent,
                    Data = "Không trùng"
                };
                return response;
            }
            else
            {
                // khởi tạo dữ liệu trả về => trả về mã 200
                var response = new BaseResponse()
                {
                    HTTPStatusCode = HTTPStatusCode.Ok,
                    Data = "Dữ liệu bị trùng"
                };
                return response;
            }
        }
    }
}
