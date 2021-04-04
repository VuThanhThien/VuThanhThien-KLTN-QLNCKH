using System;
using System.Collections.Generic;
using System.Text;

namespace QLNCKH.DL.Interface
{
    public interface IBaseDL<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetByID(Guid id);

        T GetByCode(string code);

        int Insert(T entity);

        int Update(T entity);

        int Delete(Guid id);
    }
}
