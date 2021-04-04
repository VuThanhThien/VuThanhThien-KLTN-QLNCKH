using QLNCKH.Common.Enum;
using QLNCKH.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLNCKH.DL.Base
{
    /// <summary>
    /// Lớp cơ sở tương tác CSDL
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    /// created by vtthien 08.03.2021
    public class BaseDL<T> : IBaseDL<T> where T : class
    {
        /// <summary>
        /// Biến DbContext
        /// </summary>
        protected readonly IDbContext<T> _dbContext;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="dbContext">Dapper Database Context</param>
        public BaseDL(IDbContext<T> dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Hàm lấy tất cả các bản ghi
        /// </summary>
        /// <returns>Danh sách các bản ghi</returns>
        public IEnumerable<T> GetAll()
        {
            var storeName = Procedure.GET_ALL + typeof(T).Name;
            var result = _dbContext.QueryStore(storeName);

            return result;
        }

        /// <summary>
        /// Lấy đối tượng theo ID
        /// </summary>
        /// <param name="id">ID đối tượng cần lấy</param>
        /// <returns>Đối tượng có ID trùng với ID truyền vào, không có trả về null</returns>
        public T GetByID(Guid id)
        {
            var storeName = Procedure.GET + typeof(T).Name + Procedure.BYID;

            var parameters = new
            {
                id = id.ToString()
            };

            var result = _dbContext.QueryStore(storeName, parameters).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Lấy đối tượng theo mã
        /// </summary>
        /// <param name="code">Mã</param>
        /// <returns>Đối tượng có mã Code trùng với Code cần lấy, không có trả về null</returns>
        public T GetByCode(string code)
        {
            var storeName = Procedure.GET + typeof(T).Name + Procedure.BYCode;

            var parameters = new
            {
                code = code
            };
            var result = _dbContext.QueryStore(storeName, parameters).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới</param>
        /// <returns>Số bản ghi được thêm mới lên DB</returns>
        public int Insert(T entity)
        {
            var storeName = Procedure.INSERT + typeof(T).Name;
            var result = _dbContext.ExcuteStore(storeName, entity);

            return result;
        }

        /// <summary>
        /// Cập nhật lại thông tin một bản ghi
        /// </summary>
        /// <param name="entity">Thông tin đối tượng cần cập nhật lại</param>
        /// <returns>Số bản ghi bị thay đổi dữ liệu</returns>
        public int Update(T entity)
        {
            var storeName = Procedure.UPDATE + typeof(T).Name;
            var result = _dbContext.ExcuteStore(storeName, entity);

            return result;
        }

        /// <summary>
        /// Xoá một đối tượng
        /// </summary>
        /// <param name="id">ID đối tượng cần xoá</param>
        /// <returns></returns>
        public int Delete(Guid id)
        {
            var storeName = Procedure.DELETE + typeof(T).Name;

            var parameter = new
            {
                id = id.ToString()
            };
            var result = _dbContext.ExcuteStore(storeName, parameter);

            return result;
        }

    }
}
