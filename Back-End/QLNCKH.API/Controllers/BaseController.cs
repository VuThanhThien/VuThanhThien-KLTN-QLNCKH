using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLNCKH.BL.Interface;
using System;

namespace QLNCKH.API.Controllers
{
    /// <summary>
    /// Controller dung cung
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T: class
    {
        /// <summary>
        /// Biến baseBL
        /// </summary>
        protected readonly IBaseBL<T> _baseBL;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="baseBL"></param>
        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }


        // GET: api/<BasesController>
        /// <summary>
        /// Lấy tất cả danh sách đối tượng
        /// </summary>
        /// <returns></returns>
        /// created by vtthien 08.03.2021
        [Authorize]
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            var result = _baseBL.GetAll();
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }


        // GET api/<BasesController>/5
        /// <summary>
        /// Lấy thông tin theo định danh
        /// </summary>
        /// <param name="id">Định danh đối tượng cần lấy</param>
        /// <returns>Đối tượng có id trùng với id truyền lên</returns>
        [Authorize]
        [HttpGet("{id}")]
        public virtual IActionResult GetByID([FromRoute] Guid id)
        {
            var result = _baseBL.GetByID(id);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        /// <summary>
        /// Lấy thông tin theo code
        /// </summary>
        /// <param name="code">Mã đối tượng cần lấy</param>
        /// <returns>Đối tượng có sku trùng với sku nhập vào</returns>
        [Authorize]
        [HttpGet("ByCode/{code}")]
        public virtual IActionResult GetByCode([FromRoute] string code)
        {
            var result = _baseBL.GetByCode(code);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // POST api/<BasesController>
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public virtual IActionResult Post([FromBody] T entity)
        {
            var result = _baseBL.Insert(entity);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // PUT api/<BasesController>/5
        /// <summary>
        /// Cập nhật thông tin
        /// </summary>
        /// <param name="id">Định danh đối tượng</param>
        /// <param name="entity">Đối tượng thay đổi</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public virtual IActionResult Put([FromRoute] Guid id, [FromBody] T entity)
        {
            var result = _baseBL.Update(id, entity);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }

        // DELETE api/<BasesController>/5
        /// <summary>
        /// Xoá thông tin
        /// </summary>
        /// <param name="id">Định danh thông tin cần xoá</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            var result = _baseBL.Delete(id);
            return StatusCode((int)result.HTTPStatusCode, result.Data);
        }
    }
}
