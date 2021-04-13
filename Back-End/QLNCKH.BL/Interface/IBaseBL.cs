using QLNCKH.Common;
using System;
namespace QLNCKH.BL.Interface
{
    public interface IBaseBL<T> where T : class
    {
        /// <summary>
        /// Lấy tất cả 
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        BaseResponse GetAll();

        /// <summary>
        /// Lấy theo ID
        /// </summary>
        /// <param name="id">ID của đối tượng</param>
        /// <returns>đối tượng có id thỏa mãn</returns>
        BaseResponse GetByID(Guid id);

        /// <summary>
        /// Lấy thông tin bằng mã 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        BaseResponse GetByCode(string code);

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResponse Insert(T entity);

        /// <summary>
        /// Sửa
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResponse Update(Guid id, T entity);

        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResponse Delete(Guid id);
    }
}
