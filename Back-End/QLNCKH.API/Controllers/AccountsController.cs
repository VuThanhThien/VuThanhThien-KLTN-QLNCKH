using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QLNCKH.Common.Dictionary;
using QLNCKH.Common.IdentityApplication;
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

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // POST api/<AccountsController>
        //TODO tạo mới account là tạo mới cả user
        /// <summary>
        /// Tạo tài khoản mới cho user. Đăng nhập dưới quyền admin mới làm được
        /// </summary>
        /// <param name="account">Thông tin TK</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            var user2 = new ApplicationUser()
            {
                UserName = account.UserName,
                FullName = account.FullName,
                Email = account.Email,
                EmailConfirmed = true,
            };

            var isCreated = await _userManager.CreateAsync(user2, account.Password);

            if (isCreated.Succeeded)
            {
                var result1 = await _userManager.AddToRoleAsync(user2, "User");

                if (result1.Succeeded)
                {
                    user2 = await _userManager.FindByNameAsync(account.UserName);

                    return Ok(user2);
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginParamDto">Tài khoản để Login</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginParamDto loginParamDto)
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

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));

            var token = new JwtSecurityToken(
                issuer: "http://localhost:61955",
                audience: "http://localhost:4200",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                role = userRoles
            });

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
