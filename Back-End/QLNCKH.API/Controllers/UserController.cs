
using Microsoft.AspNetCore.Mvc;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;

namespace QLNCKH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        /// <summary>
        /// Biến productBL gọi lên tầng Bussiness xử lý nghiệp vụ hàng hoá
        /// </summary>
        private readonly IUserBL _userBL;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="baseBL"></param>
        /// <param name="productBL"></param>
        public UserController(IBaseBL<User> baseBL, IUserBL userBL) : base(baseBL)
        {
            _userBL = userBL;
        }
    }
}
