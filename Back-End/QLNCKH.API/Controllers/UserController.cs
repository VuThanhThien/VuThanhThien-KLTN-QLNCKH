
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;

namespace QLNCKH.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        /// <summary>
        /// Biến productBL gọi lên tầng Bussiness xử lý nghiệp vụ hàng hoá
        /// </summary>
        private readonly IUserBL _userBL;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseBL"></param>
        /// <param name="userBL"></param>
        public UserController(IBaseBL<User> baseBL, IUserBL userBL) : base(baseBL)
        {
            _userBL = userBL;
        }

    }
}
