using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.DL.Interface
{
    public interface IBaseDL<T> where T : class
    {
        /// <summary>
        /// Lấy tất cả danh sách
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy đối tượng theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetByID(Guid id);

        /// <summary>
        /// Lấy đối tượng theo mã
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        T GetByCode(string code);

        /// <summary>
        /// Thêm mới đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(T entity);

        /// <summary>
        /// Sửa đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);

        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(Guid id);
    }
}
