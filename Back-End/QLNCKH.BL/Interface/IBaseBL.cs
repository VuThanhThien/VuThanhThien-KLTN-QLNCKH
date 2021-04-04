using QLNCKH.Common;
using System;
namespace QLNCKH.BL.Interface
{
    public interface IBaseBL<T> where T : class
    {
        BaseResponse GetAll();

        BaseResponse GetByID(Guid id);

        BaseResponse GetByCode(string code);

        BaseResponse Insert(T entity);

        BaseResponse Update(Guid id, T entity);

        BaseResponse Delete(Guid id);
    }
}
