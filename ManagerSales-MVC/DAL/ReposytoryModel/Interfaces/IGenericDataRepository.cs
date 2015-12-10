using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.ReposytoryModel.Interfaces
{
    public interface IGenericDataRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetList(Func<T, bool> where);
        T GetSingle(Func<T, bool> where);
        T GetSingle(T item);
        T FindById(int id);
        void Add(T item);
        void Add(IEnumerable<T> item);
        void Remove(T item);
        void Update(T item);
    }
}