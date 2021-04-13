
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;
using System;

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


        /// <summary>
        /// Sửa thông tin bản thân
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("MyInfo/{id}")]
        public virtual IActionResult UpdateMyInfo([FromRoute] Guid id, [FromBody] User user)
        {
            var result = _userBL.Update(id, user);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }
    }
}
