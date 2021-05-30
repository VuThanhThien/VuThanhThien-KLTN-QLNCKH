using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLNCKH.BL.Base;
using QLNCKH.BL.Interface;
using QLNCKH.Common.Dictionary;
using QLNCKH.Common.IdentityApplication;
using QLNCKH.Common.Model;
using QLNCKH.Common.NotificationCenter;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLNCKH.API.Controllers
{
    /// <summary>
    /// Quản lý tài khoản
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserBL _userBL;
        private readonly ISlackNotification _slack;
        protected readonly IBaseBL<UserInfo> _baseBL;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="baseBL"></param>
        /// <param name="userBL"></param>
        public AccountsController
            (
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager, 
            IBaseBL<UserInfo> baseBL,
            IUserBL userBL, ISlackNotification slack
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _baseBL = baseBL;
            _userBL = userBL;
            _slack = slack;
        }



        // POST api/<AccountsController>
        //TODO tạo mới account là tạo mới cả user
        /// <summary>
        /// Tạo tài khoản mới cho user. Đăng nhập dưới quyền admin mới làm được
        /// </summary>
        /// <param name="accountDto">Thông tin TK</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("Register")]
        public async Task<IActionResult> CreateAccount([FromBody] ParamRegisterDto accountDto)
        {
            var user = new ApplicationUser()
            {
                UserName = accountDto.UserName,
                FullName = accountDto.FullName,
                Email = accountDto.Email,
                EmailConfirmed = true,
            };

            // check confirm password và password
            if (accountDto.Password != accountDto.ConfirmPassword)
                return BadRequest("Xác thực mật khẩu không trùng khớp");

            // todo: nghiệp vụ check trùng
            //Trùng tên đăng nhập
            var checkUsername = await _userManager.FindByNameAsync(accountDto.UserName);
            if (checkUsername != null) {
                if (accountDto.UserName == checkUsername.UserName)
                {
                    return BadRequest("Trùng tên đăng nhập");
                }
            }

            //var  checkDupUser = _userBL.CheckDuplicatedUser(accountDto.UserCode, accountDto.IdentityCode, accountDto.PhoneNumber);
            //if(checkDupUser != null)
            //{
            //    //return BadRequest("Trùng usercode/phone/identity");
            //    if( (int)checkDupUser.HTTPStatusCode == 200)
            //        return BadRequest("Trùng mã nv hoặc sđt hoặc cmnd");
            //}

            // tạo mới một đối tượng identity
            var isCreated = await _userManager.CreateAsync(user, accountDto.Password);

            if (!isCreated.Succeeded)
                return BadRequest("Tạo mới thất bại ");

            // nếu tạo mới thành công => thêm quyền "User" cho người dùng này
            var result = await _userManager.AddToRoleAsync(user, "User");
            if (!result.Succeeded)
                return BadRequest("Không thể gán quyền User cho người dùng này");

            user = await _userManager.FindByNameAsync(accountDto.UserName);

            // tạo mới thông tin người dùng trong bảng UserInfo
            var userInfo = new UserInfo(Guid.Parse(user.Id));
            userInfo.Position = accountDto.Position;
            userInfo.UserCode = accountDto.UserCode;
            userInfo.FullName = accountDto.FullName;
            userInfo.Address = accountDto.Address;
            userInfo.DateOfBirth = accountDto.DateOfBirth;
            userInfo.Email = accountDto.Email;
            userInfo.Department = accountDto.Department;
            userInfo.PhoneNumber = accountDto.PhoneNumber;
            userInfo.IdentityCode = accountDto.IdentityCode;
            userInfo.BusinessAddress = accountDto.BusinessAddress;
            userInfo.Gender = accountDto.Gender;
            userInfo.Degree = accountDto.Degree;
            // gọi lên tầng BL để insert vào userinfo
            var insertResult = _baseBL.Insert(userInfo);

            //mapping dữ liệu trả về
            var response = new ResponseAccountDto()
            {
                UserID = userInfo.UserID,
                FullName = userInfo.FullName,
                Msg = "Đăng ký thành công"
            };

            var attach = new List<SlackAttach>()
            {
                new SlackAttach("Họ và tên: ", response.FullName, true),
                new SlackAttach("Email: ", userInfo.Email, true),
                new SlackAttach("Địa chỉ: ", userInfo.BusinessAddress, true),
                new SlackAttach("Số điện thoại: ", userInfo.PhoneNumber, true),
            };

            _ = Task.Run(() => _slack.SendSlackNotification(content: "Tạo mới tài khoản thành công!", slackAttachs: attach));
            return Ok(response);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginParamDto">Tài khoản để Login</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] ParamLoginDto loginParamDto)
        {
            // tìm thông tin user theo userName
            var user = await _userManager.FindByNameAsync(loginParamDto.UserName);

            // Không tìm thấy user => trả về 400
            if (user == null)
                return BadRequest("UserName không tồn tại");

            // kiểm tra mật khẩu truyền lên
            var isCheckedPassword = await _userManager.CheckPasswordAsync(user, loginParamDto.Password);

            // mật khẩu không trùng khớp ==> trả về 400
            if (!isCheckedPassword)
                return BadRequest("Mật khẩu không chính xác");

            // lấy role của người dùng
            var userRoles = await _userManager.GetRolesAsync(user);


            // tạo một đối tượng userinfo -> query để lấy thông tin
            var response = new ResponseLoginDto()
            {
                Token = JWTHelper.GenAccessToken(user, userRoles),
                Role = userRoles.FirstOrDefault(),
                FullName = user.FullName,
                UserID = Guid.Parse( user.Id),
            };
            return Ok(response);

        }

        /// <summary>
        /// API Đăng xuất
        /// </summary>
        /// <returns></returns>
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok("Đã logout");
        }
    }
}
